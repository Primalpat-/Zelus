using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data;
using Zelus.Web.Helpers.PlayerMods;

namespace Zelus.Web.Models.Synchronization.Scrapers
{
    public class ModScraper
    {
        public List<PlayerMod> Execute(ZelusDbContext db)
        {
            var results = new List<PlayerMod>();
            var timeFilter = DateTime.UtcNow.AddHours(-18);
            var players = db.Players
                            .Where(p => p.ModSyncEnabled &&
                                        p.LastSync < timeFilter)
                            .OrderBy(p => p.LastSync)
                            .ToList();

            foreach (var player in players)
            {
                var modContainers = GetModContainers(player);

                if (modContainers == null)
                    return results;

                foreach (var container in modContainers)
                {
                    var mod = container.ParseMod(db, player.Id);
                    results.Add(mod);
                }
            }
        }

        private List<HtmlNode> GetModContainers(Player player)
        {
            try
            {
                var results = new List<HtmlNode>();
                var web = new HtmlWeb();
                for (var i = 0; i < 100; i++)
                {
                    var currentModPage = web.Load($"{player.SwgohGgUrl}mods/?page={i}");

                    if (currentModPage == null || currentModPage.DocumentNode.ChildNodes.Count == 0)
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