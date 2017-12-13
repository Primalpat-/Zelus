using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data;
using Zelus.Logic.Extensions.Scraping.PlayerMods;

namespace Zelus.Logic.Synchronization.Scrapers
{
    public class ModScraper
    {
        public List<PlayerMod> Execute(ZelusDbContext db)
        {
            var results = new List<PlayerMod>();
            var timeFilter = DateTime.UtcNow.AddHours(-18);
            var units = db.Units.ToList();
            var players = db.Players.Where(p => p.ModSyncEnabled &&
                                        p.LastModSync < timeFilter)
                            .Include(p => p.PlayerCharacters)
                            .OrderBy(p => p.LastModSync)
                            .ToList();

            foreach (var player in players)
            {
                var modContainers = GetModContainers(player);

                if (modContainers == null)
                    return results;

                foreach (var container in modContainers)
                {
                    var mod = container.ParseMod(player, units);
                    results.Add(mod);
                }
            }

            return results;
        }

        private List<HtmlNode> GetModContainers(Player player)
        {
            try
            {
                var results = new List<HtmlNode>();
                var web = new HtmlWeb();
                for (var i = 1; i < 100; i++)
                {
                    var currentModPage = web.Load($"{player.SwgohGgUrl}mods/?page={i}");

                    if (web.StatusCode != HttpStatusCode.OK ||
                        currentModPage == null || 
                        currentModPage.DocumentNode.ChildNodes.Count == 0)
                        return results;

                    var containers = currentModPage.DocumentNode?
                                                   .Descendants("div")
                                                   .Where(d => d.Attributes["class"].IsNotNull() &&
                                                               d.Attributes["class"].Value.Contains("collection-mod"))
                                                   .ToList();
                    results.AddRange(containers);
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed when retrieving mod containers.");
            }
        }
    }
}