using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.BulkExtensions;
using Ether.Outcomes;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data.Models;
using Zelus.Web.Models.Helpers;

namespace Zelus.Web.Models.Synchronization
{
    public static class PlayerCharacterSynchronizer
    {
        private static ZelusContext _db;
        private static List<Character> _savedCharacters;
        private static List<CharacterZeta> _zetaList;

        public static IOutcome Execute(int guildId)
        {
            try
            {
                _db = new ZelusContext();
                _savedCharacters = _db.Characters.ToList();
                _zetaList = _db.CharacterZetas.ToList();

                var guild = _db.Guilds.Find(guildId);
                var playerCharacters = new List<PlayerCharacter>();

                foreach (var player in guild.Players)
                {
                    var characterOutcome = GetUpdatedPlayerCharacters(player);
                    if (characterOutcome.Failure)
                        return Outcomes.Failure()
                                       .WithMessagesFrom(characterOutcome);

                    playerCharacters.AddRange(characterOutcome.Value);
                }
                
                _db.BulkInsertOrUpdate(playerCharacters);

                return Outcomes.Success();
            }
            catch (Exception ex)
            {
                return Outcomes.Failure()
                               .WithMessage(ex.Message);
            }
        }

        public static IOutcome Execute(int playerId, HtmlDocument doc)
        {
            try
            {
                _db = new ZelusContext();
                _savedCharacters = _db.Characters.ToList();
                _zetaList = _db.CharacterZetas.ToList();

                var player = _db.Players.Single(p => p.Id == playerId);
                var characterOutcome = GetUpdatedPlayerCharacters(player, doc);
                if (characterOutcome.Failure)
                    return Outcomes.Failure()
                                   .WithMessagesFrom(characterOutcome);

                _db.BulkInsertOrUpdate(characterOutcome.Value);

                return Outcomes.Success();
            }
            catch (Exception ex)
            {
                return Outcomes.Failure()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<List<PlayerCharacter>> GetUpdatedPlayerCharacters(Player player, HtmlDocument doc = null)
        {
            try
            {
                if (doc.IsNull())
                {
                    var web = new HtmlWeb();
                    doc = web.Load(player.CollectionUrl);
                }
                
                var characterContainers = doc.DocumentNode
                                             .Descendants("a")
                                             .Where(d => d.Attributes["class"].IsNotNull() &&
                                                         d.Attributes["class"].Value.Contains("char-portrait-full-link"))
                                             .ToList();
                var playerCharacters = new List<PlayerCharacter>();

                foreach (var container in characterContainers)
                {
                    var characterOutcome = GetUpdatedPlayerCharacter(container, player);
                    if (characterOutcome.Failure)
                        return Outcomes.Failure<List<PlayerCharacter>>()
                                       .WithMessagesFrom(characterOutcome);
                        
                    playerCharacters.Add(characterOutcome.Value);
                }

                return Outcomes.Success<List<PlayerCharacter>>()
                               .WithValue(playerCharacters);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<List<PlayerCharacter>>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<PlayerCharacter> GetUpdatedPlayerCharacter(HtmlNode container, Player player)
        {
            try
            {
                var characterOutcome = CharacterSynchronizer.CreateCharacter(_db, container, _savedCharacters);
                if (characterOutcome.Failure)
                    return Outcomes.Failure<PlayerCharacter>()
                                   .WithMessagesFrom(characterOutcome);

                var gearOutcome = GetCharacterGearLevel(container);
                if (gearOutcome.Failure)
                    return Outcomes.Failure<PlayerCharacter>()
                                   .WithMessagesFrom(gearOutcome);

                var starOutcome = GetCharacterStarLevel(container);
                if (starOutcome.Failure)
                    return Outcomes.Failure<PlayerCharacter>()
                                   .WithMessagesFrom(starOutcome);

                var levelOutcome = GetCharacterLevel(container);
                if (levelOutcome.Failure)
                    return Outcomes.Failure<PlayerCharacter>()
                                   .WithMessagesFrom(levelOutcome);

                var urlOutcome = GetCharacterUrl(container);
                if (urlOutcome.Failure)
                    return Outcomes.Failure<PlayerCharacter>()
                                   .WithMessagesFrom(urlOutcome);

                var extendedOutcome = CharacterSynchronizer.GetCharacterExtendedStats(characterOutcome.Value,
                                                                                      levelOutcome.Value,
                                                                                      urlOutcome.Value, 
                                                                                      _zetaList);
                if (extendedOutcome.Failure)
                    return Outcomes.Failure<PlayerCharacter>()
                                   .WithMessagesFrom(extendedOutcome);

                var playerCharacter = player.PlayerCharacters
                                            .FirstOrDefault(pc => pc.CharacterId == characterOutcome.Value.Id);

                if (playerCharacter.IsNull())
                {
                    playerCharacter = new PlayerCharacter();
                    playerCharacter.PlayerId = player.Id;
                    playerCharacter.CharacterId = characterOutcome.Value.Id;
                }
                
                playerCharacter.GearLevel = gearOutcome.Value;
                playerCharacter.StarLevel = starOutcome.Value;
                playerCharacter.CharacterLevel = levelOutcome.Value;
                playerCharacter.CharacterUrl = urlOutcome.Value;
                playerCharacter.NumberOfZetas = extendedOutcome.Value.NumberOfZetas;
                playerCharacter.PowerLevel = extendedOutcome.Value.PowerLevel;
                
                return Outcomes.Success<PlayerCharacter>()
                               .WithValue(playerCharacter);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<PlayerCharacter>()
                               .WithMessage(ex.Message);
            }
        }

        #region Character Parsing Helpers

        private static IOutcome<string> GetCharacterUrl(HtmlNode container)
        {
            try
            {
                var url = "https://swgoh.gg" + container.Attributes["href"].Value;
                return Outcomes.Success<string>()
                               .WithValue(url);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<string>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<int> GetCharacterLevel(HtmlNode container)
        {
            try
            {
                var level = container.Descendants("div")
                                     .First(d => d.Attributes["class"].Value.Contains("char-portrait-full-level"))
                                     .InnerText;

                return Outcomes.Success<int>()
                               .WithValue(level.ToInt32());
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<int>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<int> GetCharacterStarLevel(HtmlNode container)
        {
            try
            {
                var inactiveStars = container.Descendants("div")
                                             .Count(d => d.Attributes["class"].Value.Contains("star-inactive"));

                return Outcomes.Success<int>()
                               .WithValue(7 - inactiveStars);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<int>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<int> GetCharacterGearLevel(HtmlNode container)
        {
            try
            {
                var gearLevel = container.Descendants("div")
                                         .First(d => d.Attributes["class"].Value.Contains("char-portrait-full-gear-level"))
                                         .InnerText
                                         .ToLevel();

                return Outcomes.Success<int>()
                               .WithValue(gearLevel);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<int>()
                               .WithMessage(ex.Message);
            }
        }

        #endregion
    }
}