using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Zelus.Data;
using Zelus.Web.Helpers.GuildHome;

namespace Zelus.Web.Models.Synchronization.Scrapers
{
    public class PlayerScraper
    {
        /// <summary>
        /// Using the guild's home page url, this function will load and parse all the player entities out
        /// </summary>
        public List<Player> Execute(ZelusDbContext db)
        {
            var results = new List<Player>();
            var web = new HtmlWeb();
            var guilds = db.Guilds.ToList();

            foreach (var guild in guilds)
            {
                var guildHome = web.Load(guild.SwgohGgUrl);
                var playerRows = guildHome.DocumentNode
                                          .Descendants("tbody")
                                          .First()
                                          .Descendants("tr")
                                          .ToList();

                foreach (var row in playerRows)
                {
                    var player = row.Descendants("td").ParsePlayer(guild.Id);
                    results.Add(player);
                }
            }

            return results;
        }
    }
}