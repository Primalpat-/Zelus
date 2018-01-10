using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data;

namespace Zelus.Logic.Extensions.Scraping.PlayerMods
{
    public static class ModContainerExtensions
    {
        public static PlayerMod ParseMod(this HtmlNode container, Player player, List<Unit> units)
        {
            var mod = new PlayerMod();

            mod.SwgohGgId = container.ParseId();
            mod.PlayerId = player.Id;
            mod.PlayerCharacterId = container.ParsePlayerCharacter(player, units);
            mod.Pips = container.ParsePips();
            mod.SlotId = container.ParseSlot();
            mod.SetId = container.ParseSet();

            return container.ParseStats(mod);
        }

        private static string ParseId(this HtmlNode container)
        {
            var id = container.Attributes["data-id"].Value;
            return id;
        }

        private static int? ParsePlayerCharacter(this HtmlNode container, Player player, List<Unit> units)
        {
            var unitImage = container.Descendants("img")
                                     .First(d => d.Attributes["class"].IsNotNull() &&
                                                 d.Attributes["class"].Value.Contains("char-portrait-img"));

            var unitName = HttpUtility.HtmlDecode(unitImage.Attributes["alt"].Value);
            var unit = units.Single(u => string.Compare(u.Name, unitName, StringComparison.OrdinalIgnoreCase) == 0);
            var playerCharacter = player.PlayerCharacters.FirstOrDefault(pc => pc.UnitId == unit.Id);

            return playerCharacter?.Id;
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

        private static PlayerMod ParseStats(this HtmlNode container, PlayerMod mod)
        {
            var primaryStatContainer = container.Descendants("div")
                                                .First(d => d.Attributes["class"].IsNotNull() &&
                                                            d.Attributes["class"].Value.Contains("statmod-stats-1"));

            mod.PrimaryTypeId = GetTypeFromLabel(primaryStatContainer.GetStatLabel());
            mod.PrimaryUnitsId = GetUnitsFromValue(primaryStatContainer.GetStatValue());
            mod.PrimaryValue = GetActualValue(primaryStatContainer.GetStatValue());

            var secondaryStatContainers = container.Descendants("div")
                                                   .First(d => d.Attributes["class"].IsNotNull() &&
                                                               d.Attributes["class"].Value.Contains("statmod-stats-2"))
                                                   .Elements("div")
                                                   .ToList();

            //First Secondary Stat
            mod.Secondary1TypeId = GetTypeFromLabel(secondaryStatContainers.Count > 0 ? 
                                                    secondaryStatContainers[0].GetStatLabel() :
                                                    null);
            mod.Secondary1UnitsId = GetUnitsFromValue(secondaryStatContainers.Count > 0 ? 
                                                      secondaryStatContainers[0].GetStatValue():
                                                      null);
            mod.Secondary1Value = GetActualValue(secondaryStatContainers.Count > 0 ?
                                                 secondaryStatContainers[0].GetStatValue() : 
                                                 null);
            //Second Secondary Stat
            mod.Secondary2TypeId = GetTypeFromLabel(secondaryStatContainers.Count > 1 ?
                                                    secondaryStatContainers[1].GetStatLabel() :
                                                    null);
            mod.Secondary2UnitsId = GetUnitsFromValue(secondaryStatContainers.Count > 1 ?
                                                      secondaryStatContainers[1].GetStatValue() :
                                                      null);
            mod.Secondary2Value = GetActualValue(secondaryStatContainers.Count > 1 ?
                                                 secondaryStatContainers[1].GetStatValue() :
                                                 null);
            //Third Secondary Stat
            mod.Secondary3TypeId = GetTypeFromLabel(secondaryStatContainers.Count > 2 ?
                                                    secondaryStatContainers[2].GetStatLabel() :
                                                    null);
            mod.Secondary3UnitsId = GetUnitsFromValue(secondaryStatContainers.Count > 2 ?
                                                      secondaryStatContainers[2].GetStatValue() :
                                                      null);
            mod.Secondary3Value = GetActualValue(secondaryStatContainers.Count > 2 ?
                                                 secondaryStatContainers[2].GetStatValue() :
                                                 null);
            //Fourth Secondary Stat
            mod.Secondary4TypeId = GetTypeFromLabel(secondaryStatContainers.Count > 3 ?
                                                    secondaryStatContainers[3].GetStatLabel() :
                                                    null);
            mod.Secondary4UnitsId = GetUnitsFromValue(secondaryStatContainers.Count > 3 ?
                                                      secondaryStatContainers[3].GetStatValue() :
                                                      null);
            mod.Secondary4Value = GetActualValue(secondaryStatContainers.Count > 3 ?
                                                 secondaryStatContainers[3].GetStatValue() :
                                                 null);

            return mod;
        }

        private static string GetStatLabel(this HtmlNode statsContainer)
        {
            return statsContainer.Descendants("span")
                                 .First(d => d.Attributes["class"].Value.Contains("statmod-stat-label"))
                                 .InnerText;
        }

        private static string GetStatValue(this HtmlNode statsContainer)
        {
            return statsContainer.Descendants("span")
                                 .First(d => d.Attributes["class"].Value.Contains("statmod-stat-value"))
                                 .InnerText;
        }

        private static int GetTypeFromLabel(string typeName)
        {
            if (typeName.IsNullOrEmpty())
                return (int) ModStatTypes.Undefined;

            if (string.Compare(typeName, "Offense", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Offense;

            if (string.Compare(typeName, "Defense", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Defense;

            if (string.Compare(typeName, "Potency", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Potency;

            if (string.Compare(typeName, "Tenacity", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Tenacity;

            if (string.Compare(typeName, "Critical Chance", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Critical_Chance;

            if (string.Compare(typeName, "Critical Damage", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Critical_Damage;

            if (string.Compare(typeName, "Critical Avoidence", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Critical_Avoidence;

            if (string.Compare(typeName, "Health", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Health;

            if (string.Compare(typeName, "Protection", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Protection;

            if (string.Compare(typeName, "Accuracy", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Accuracy;

            if (string.Compare(typeName, "Speed", StringComparison.OrdinalIgnoreCase) == 0)
                return (int) ModStatTypes.Speed;

            return (int) ModStatTypes.Undefined;
        }

        private static int GetUnitsFromValue(string value)
        {
            if (value.IsNullOrEmpty())
                return (int) ModStatUnits.Undefined;

            if (value.Contains("%"))
                return (int) ModStatUnits.Percentage;

            return (int) ModStatUnits.Flat;
        }

        private static decimal GetActualValue(string value)
        {
            if (value.IsNullOrEmpty())
                return default(decimal);

            return value.Replace("+", "")
                        .Replace("%", "")
                        .ToDecimalOrDefault();
        }
    }
}