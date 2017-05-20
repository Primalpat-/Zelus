using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Ether.Outcomes;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data.Models;
using Zelus.Web.Models.Extensions;
using Zelus.Web.Models.Synchronization.Models;

namespace Zelus.Web.Models.Synchronization
{
    public static class CharacterSynchronizer
    {
        public static IOutcome<Character> CreateCharacter(ZelusContext db, HtmlNode container, List<Character> savedCharacters)
        {
            try
            {
                var image = container.Descendants("img").First();
                var characterName = image.Attributes["alt"].Value;
                var character = savedCharacters.FirstOrDefault(c => c.Name.ToLower() == characterName.ToLower());

                if (character.IsNull())
                {
                    using (var client = new WebClient())
                    {
                        character = new Character
                        {
                            Name = characterName,
                            Portrait = client.DownloadData("https:" + image.Attributes["src"].Value)
                        };
                    }
                    db.Characters.Add(character);
                    db.SaveChanges();
                }

                return Outcomes.Success<Character>()
                               .WithValue(character);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<Character>()
                               .WithMessage(ex.Message);
            }
        }

        public static IOutcome<ExtendedCharacterStatsModel> GetCharacterExtendedStats(Character character, int characterLevel, string characterUrl, List<CharacterZeta> zetaList)
        {
            try
            {
                var model = new ExtendedCharacterStatsModel();

                if (!character.CanHaveZeta(characterLevel))
                    return Outcomes.Success<ExtendedCharacterStatsModel>()
                                   .WithValue(model);

                var web = new HtmlWeb();
                var doc = web.Load(characterUrl);

                model.PowerLevel = doc.DocumentNode
                                      .Descendants("div")
                                      .First(d => d.Attributes["class"].IsNotNull() &&
                                                  d.Attributes["class"].Value == "media-heading" &&
                                                  d.InnerText.Contains("Power", StringComparison.CurrentCultureIgnoreCase))
                                      .FirstChild
                                      .InnerText
                                      .ToInt32();

                model.NumberOfZetas = doc.DocumentNode
                                         .Descendants("a")
                                         .Count(a => a.Attributes["class"].IsNotNull() &&
                                                     a.Attributes["class"].Value == "pc-skill-link" &&
                                                     zetaList.Any(z => z.SkillName.ToLower() == a.LastChild.InnerText.ToLower()) &&
                                                     a.FirstChild.Attributes["data-title"].Value.Contains("Maxed", StringComparison.CurrentCultureIgnoreCase));
                    

                return Outcomes.Success<ExtendedCharacterStatsModel>()
                               .WithValue(model);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<ExtendedCharacterStatsModel>()
                               .WithMessage(ex.Message);
            }
        }
    }
}