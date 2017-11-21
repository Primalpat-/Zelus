using System;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data;

namespace Zelus.Web.Helpers.PlayerCollection
{
    public static class CharacterContainerExtensions
    {
        public static PlayerCharacter ParseCharacter(this HtmlNode container, ZelusDbContext db, int playerId)
        {
            var character = new PlayerCharacter();

            character.PlayerId = playerId;
            character.UnitId = container.ParseUnitId(db);
            character.Gear = container.ParseGear();
            character.Level = container.ParseLevel();
            character.Power = container.ParsePower();
            character.Stars = container.ParseStars();

            return character;
        }

        private static int ParseUnitId(this HtmlNode container, ZelusDbContext db)
        {
            var unitImage = container.Descendants("img")
                                     .First(d => d.Attributes["class"].IsNotNull() &&
                                                 (d.Attributes["class"].Value.Contains("char-portrait-img") ||
                                                  d.Attributes["class"].Value.Contains("char-portrait-full-img")));

            if (unitImage == null)
            {
                var test = "";
            }

            var unitName = HttpUtility.HtmlDecode(unitImage.Attributes["alt"].Value);
            var unit = db.Units.Single(u => string.Compare(u.Name, unitName, StringComparison.OrdinalIgnoreCase) == 0);

            return unit.Id;
        }

        private static int ParseGear(this HtmlNode container)
        {
            var gearContainer = container.Descendants("div")
                                         .First(d => d.Attributes["class"].IsNotNull() &&
                                                     d.Attributes["class"].Value.Contains("char-portrait-full-gear-level"));

            var gear = gearContainer.InnerText.ToLevel();

            return gear;
        }

        private static int ParseLevel(this HtmlNode container)
        {
            var levelContainer = container.Descendants("div")
                                          .First(d => d.Attributes["class"].IsNotNull() &&
                                                      d.Attributes["class"].Value.Contains("char-portrait-full-level"));

            var gear = levelContainer.InnerText.ToInt32();

            return gear;
        }

        private static int ParseStars(this HtmlNode container)
        {
            var inactiveStars = container.Descendants("div")
                                         .Count(d => d.Attributes["class"].Value.Contains("star-inactive"));

            var stars = 7 - inactiveStars;

            return stars;
        }

        private static int ParsePower(this HtmlNode container)
        {
            var powerContainer = container.Descendants("div")
                                          .First(d => d.Attributes["class"].IsNotNull() &&
                                                      d.Attributes["class"].Value.Contains("collection-char-gp"));

            var powerText = powerContainer.Attributes["title"].Value;
            var power = powerText.Split('/')[0].ReduceToDigits();

            return power;
        }
    }
}