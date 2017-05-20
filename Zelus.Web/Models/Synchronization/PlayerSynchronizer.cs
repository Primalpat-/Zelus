using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.BulkExtensions;
using Ether.Outcomes;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data.Models;

namespace Zelus.Web.Models.Synchronization
{
    public static class PlayerSynchronizer
    {
        public static IOutcome Execute(int guildId)
        {
            try
            {
                var db = new ZelusContext();
                var guild = db.Guilds.Find(guildId);
                var sync = guild.GuildSynchronizations.OrderByDescending(s => s.Timestamp)
                                                      .First();

                var doc = new HtmlDocument();
                doc.LoadHtml(sync.Data);

                var playerRows = doc.DocumentNode.Descendants("tbody")
                                 .First()
                                 .Descendants("tr")
                                 .ToList();

                var players = new List<Player>();
                foreach (var playerRow in playerRows)
                {
                    var playerOutcome = GetUpdatedPlayer(guild, playerRow);
                    if (playerOutcome.Failure)
                        return Outcomes.Failure()
                                       .WithMessagesFrom(playerOutcome);

                    players.Add(playerOutcome.Value);
                }

                db.BulkInsertOrUpdate(players);

                return Outcomes.Success();
            }
            catch (Exception ex)
            {
                return Outcomes.Failure()
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
                                  .FirstOrDefault(p => p.Name.ToLower() == data.ElementAt(0).InnerText.ToLower());

                if (player.IsNull())
                {
                    player = new Player();
                    player.GuildId = guild.Id;
                }

                player.Name = data.ElementAt(0).InnerText;
                player.CollectionUrl = urlOutcome.Value;
                player.PlayerLevel = data.ElementAt(1).InnerText.ToInt32();
                player.ArenaRank = data.ElementAt(2).InnerText.ToInt32();
                player.ArenaAverage = data.ElementAt(3).InnerText.ToInt32();
                player.CollectionScore = data.ElementAt(4).InnerText.ToDecimal();
                player.GuildCurrency = data.ElementAt(5).InnerText.ToInt32OrDefault();

                return Outcomes.Success<Player>()
                               .WithValue(player);
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
                var userLink = node.FirstChild.Attributes["href"].Value;
                var userName = userLink.Split("/")[2];
                var collectionUrl = "https://swgoh.gg/u/" + userName + "/collection/";

                return Outcomes.Success<string>()
                               .WithValue(collectionUrl);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<string>()
                               .WithMessage(ex.Message);
            }
        }
    }
}