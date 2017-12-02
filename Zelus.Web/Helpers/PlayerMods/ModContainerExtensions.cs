using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data;

namespace Zelus.Web.Helpers.PlayerMods
{
    public static class ModContainerExtensions
    {
        public static PlayerMod ParseMod(this HtmlNode container, ZelusDbContext db, int playerId)
        {
            var mod = new PlayerMod();

            mod.PlayerId = playerId;
            mod.PlayerCharacterId = container.ParsePlayerCharacter(db, playerId);
            mod.Pips = container.ParsePips();
            mod.SlotId = container.ParseSlot();
            mod.SetId = container.ParseSet();

            var stats = container.ParseStats();

            return mod;
        }

        private static int ParsePlayerCharacter(this HtmlNode container, ZelusDbContext db, int playerId)
        {
            var unitImage = container.Descendants("img")
                                     .First(d => d.Attributes["class"].IsNotNull() &&
                                                 d.Attributes["class"].Value.Contains("char-portrait-img"));

            var unitName = HttpUtility.HtmlDecode(unitImage.Attributes["alt"].Value);
            var unit = db.Units.Single(u => string.Compare(u.Name, unitName, StringComparison.OrdinalIgnoreCase) == 0);
            var playerCharacter = db.PlayerCharacters.Single(pc => pc.PlayerId == playerId && pc.UnitId == unit.Id);

            return playerCharacter.Id;
        }

        private static int ParsePips(this HtmlNode container)
        {
            var pipsContainer = container.Descendants("span")
                                         .First(d => d.Attributes["class"].IsNotNull() &&
                                                     d.Attributes["class"].Value.Contains("statmod-pips"));

            return pipsContainer.Descendants("span").Count();
        }

        private static int ParseSlot(this HtmlNode container)
        {
            var modContainer = container.Element("div");

            if (modContainer.Attributes["class"].Value.Contains("pc-statmod-slot1"))
                return (int)ModSlots.Square;

            if (modContainer.Attributes["class"].Value.Contains("pc-statmod-slot2"))
                return (int)ModSlots.Arrow;

            if (modContainer.Attributes["class"].Value.Contains("pc-statmod-slot3"))
                return (int)ModSlots.Diamond;

            if (modContainer.Attributes["class"].Value.Contains("pc-statmod-slot4"))
                return (int)ModSlots.Triangle;

            if (modContainer.Attributes["class"].Value.Contains("pc-statmod-slot5"))
                return (int)ModSlots.Circle;

            if (modContainer.Attributes["class"].Value.Contains("pc-statmod-slot6"))
                return (int)ModSlots.Cross;

            return 0;
        }

        private static int ParseSet(this HtmlNode container)
        {
            var modImage = container.Descendants("img")
                                    .First(d => d.Attributes["class"].IsNotNull() &&
                                                d.Attributes["class"].Value.Contains("statmod-img"));

            var modName = HttpUtility.HtmlDecode(modImage.Attributes["alt"].Value);

            if (modName.Contains("Health"))
                return (int) ModSets.Health;

            if (modName.Contains("Defense"))
                return (int)ModSets.Defense;

            if (modName.Contains("Crit Damage"))
                return (int)ModSets.Crit_Damage;

            if (modName.Contains("Crit Chance"))
                return (int)ModSets.Crit_Chance;

            if (modName.Contains("Tenacity"))
                return (int)ModSets.Tenacity;

            if (modName.Contains("Offense"))
                return (int)ModSets.Offense;

            if (modName.Contains("Potency"))
                return (int)ModSets.Potency;

            if (modName.Contains("Speed"))
                return (int)ModSets.Speed;

            return 0;
        }

        private static object ParseStats(this HtmlNode container)
        {
            return new {};
        }
    }
}