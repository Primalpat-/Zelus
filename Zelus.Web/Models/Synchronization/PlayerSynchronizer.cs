using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using EntityFramework.BulkExtensions;
using Ether.Outcomes;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data.Models;
using Zelus.Web.Models.Extensions;
using Zelus.Web.Models.Helpers;
using Zelus.Web.Models.Synchronization.Models;

namespace Zelus.Web.Models.Synchronization
{
    public static class PlayerSynchronizer
    {
        private static ZelusContext _db;

        public static IOutcome Execute(int guildId, HtmlDocument doc)
        {
            try
            {
                _db = new ZelusContext();
                var guild = _db.Guilds.Find(guildId);
                var playerRows = doc.DocumentNode.Descendants("tbody")
                                 .First()
                                 .Descendants("tr")
                                 .ToList();

                var upToDatePlayers = new List<Player>();
                foreach (var playerRow in playerRows)
                {
                    var playerOutcome = GetUpdatedPlayer(guild, playerRow);
                    if (playerOutcome.Failure)
                        return Outcomes.Failure()
                                       .WithMessagesFrom(playerOutcome);

                    upToDatePlayers.Add(playerOutcome.Value);
                }

                var playersToRemove = new List<Player>();
                foreach (var player in guild.Players)
                {
                    var playerStillInGuild = upToDatePlayers.Any(p => p.CollectionUrl.IsSimilarTo(player.CollectionUrl));
                    if (!playerStillInGuild)
                    {
                        player.GuildId = PlaceholderGuild.Get();
                        playersToRemove.Add(player);
                    }
                }

                _db.BulkUpdate(playersToRemove);
                _db.BulkInsertOrUpdate(upToDatePlayers);

                return Outcomes.Success();
            }
            catch (Exception ex)
            {
                return Outcomes.Failure()
                               .WithMessage(ex.Message);
            }
        }

        public static IOutcome<PlayerSynchronizationResult> Execute(Player player, string playerUsername)
        {
            try
            {
                _db = new ZelusContext();

                var model = new PlayerSynchronizationResult();
                var collectionUrl = playerUsername.ToCollectionUrl();

                var web = new HtmlWeb();
                model.Document = web.Load(collectionUrl);

                var guildOutcome = GuildSynchronizer.CreateOrUpdateGuild(model.Document);
                if (guildOutcome.Failure)
                    return Outcomes.Failure<PlayerSynchronizationResult>()
                                   .WithMessagesFrom(guildOutcome);

                var playerOutcome = GetUpdatedPlayer(player, guildOutcome.Value.Id, collectionUrl, model.Document);
                if (playerOutcome.Failure)
                    return Outcomes.Failure<PlayerSynchronizationResult>()
                                   .WithMessagesFrom(playerOutcome);

                _db.Players.AddOrUpdate(playerOutcome.Value);
                _db.SaveChanges();

                model.Player = playerOutcome.Value;

                var syncOutcome = CreateNewSynchronization(playerOutcome.Value);
                if (syncOutcome.Failure)
                    return Outcomes.Failure<PlayerSynchronizationResult>()
                                   .WithMessagesFrom(syncOutcome);

                return Outcomes.Success<PlayerSynchronizationResult>()
                               .WithValue(model);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<PlayerSynchronizationResult>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<PlayerSynchronization> CreateNewSynchronization(Player player)
        {
            try
            {
                var sync = new PlayerSynchronization
                {
                    PlayerId = player.Id,
                    Timestamp = DateTime.UtcNow
                };

                _db.PlayerSynchronizations.Add(sync);
                _db.SaveChanges();

                return Outcomes.Success<PlayerSynchronization>()
                               .WithValue(sync);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<PlayerSynchronization>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<Player> GetUpdatedPlayer(Player player, int guildId, string collectionUrl, HtmlDocument doc)
        {
            try
            {
                var nameOutcome = ParsePlayerName(doc);
                if (nameOutcome.Failure)
                    return Outcomes.Failure<Player>()
                                   .WithMessagesFrom(nameOutcome);

                var levelOutcome = ParsePlayerLevel(doc);
                if (levelOutcome.Failure)
                    return Outcomes.Failure<Player>()
                                   .WithMessagesFrom(levelOutcome);

                return GetUpdatedPlayer(player,
                                        guildId,
                                        collectionUrl,
                                        nameOutcome.Value,
                                        level: levelOutcome.Value);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<Player>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<Player> GetUpdatedPlayer(Player player, int guildId, string collectionUrl, string name, int level = 0, int gp = 0)
        {
            try
            {
                if (player.IsNull())
                {
                    player = new Player();
                    player.GuildId = guildId;
                    player.CollectionUrl = collectionUrl;
                }

                player.Name = HttpUtility.HtmlDecode(name);

                if (level > 0)
                    player.Level = level;

                if (gp > 0)
                    player.GalacticPower = gp;

                return Outcomes.Success<Player>()
                               .WithValue(player);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<Player>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<Player> GetUpdatedPlayer(Guild guild, HtmlNode node)
        {
            try
            {
                var data = node.Descendants("td");
                var urlOutcome = ParseCollectionUrl(data.ElementAt(0));
                if (urlOutcome.Failure)
                    return Outcomes.Failure<Player>()
                                   .WithMessagesFrom(urlOutcome);

                var player = guild.Players
                                  .FirstOrDefault(p => p.CollectionUrl.ToLower() == urlOutcome.Value.ToLower());

                var playerName = HttpUtility.HtmlDecode(data.ElementAt(0).InnerText.Replace("\n", ""));
                var playerGp = data.ElementAt(1).InnerText.ToInt32();

                return GetUpdatedPlayer(player, 
                                        guild.Id, 
                                        urlOutcome.Value,
                                        playerName,
                                        gp: playerGp);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<Player>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<string> ParseCollectionUrl(HtmlNode node)
        {
            try
            {
                var userLink = node.ChildNodes[1].Attributes["href"].Value;
                var userName = userLink.Split("/")[2];

                return Outcomes.Success<string>()
                               .WithValue(userName.ToCollectionUrl());
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<string>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<string> ParsePlayerName(HtmlDocument doc)
        {
            try
            {
                var name = doc.DocumentNode
                              .Descendants("a")
                              .Last(a => a.Attributes["class"].IsNotNull() &&
                                         a.Attributes["class"].Value == "no-decoration char-name")
                              .InnerText;

                return Outcomes.Success<string>()
                               .WithValue(HttpUtility.HtmlDecode(name));
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<string>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<int> ParsePlayerLevel(HtmlDocument doc)
        {
            try
            {
                var level = doc.DocumentNode
                               .Descendants("li")
                               .ToList()
                               .First(li => li.InnerText.IsNotNullOrEmpty() &&
                                            li.InnerText.Contains("Level"))
                               .ChildNodes["h5"]
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
    }
}