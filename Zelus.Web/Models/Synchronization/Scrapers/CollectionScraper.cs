using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Zelus.Data;
using Zelus.Web.Helpers.PlayerCollection;

namespace Zelus.Web.Models.Synchronization.Scrapers
{
    public class CollectionScraper
    {
        public List<PlayerCharacter> Execute(ZelusDbContext db)
        {
            var results = new List<PlayerCharacter>();
            var timeFilter = DateTime.UtcNow.AddHours(-18);
            var units = db.Units.ToList();
            var players = db.Players
                            .Where(p => p.CollectionSyncEnabled &&
                                        p.LastCollectionSync < timeFilter)
                            .OrderBy(p => p.LastCollectionSync)
                            .ToList();

            foreach (var player in players)
            {
                var characterContainers = GetCharacterNodes(player);

                if (characterContainers == null)
                    return results;

                foreach (var container in characterContainers)
                {
                    var character = container.ParseCharacter(units, player.Id);
                    results.Add(character);
                }
            }

            return results;
        }

        private List<HtmlNode> GetCharacterNodes(Player player)
        {
            try
            {
                var characterContainers = new List<HtmlNode>();
                var sleepTimer = 1000;
                while (characterContainers.Count == 0)
                {
                    var playerCollection = GetPlayerCollection(player);

                    if (playerCollection == null)
                        return null;

                    characterContainers = playerCollection.DocumentNode?
                                                          .SelectSingleNode("//*[contains(concat(\" \", normalize-space(@class), \" \"), \" collection-char-list \")]")?
                                                          .Element("div")?                                                //Get the child row
                                                          .Elements("div")?                                               //Get the col children
                                                          .Select(e => e.Element("div"))?                                 //Select the collection-char
                                                          .Where(d => !d.Attributes["class"].Value.Contains("missing"))?  //Filter out the locked characters
                                                          .ToList();

                    System.Threading.Thread.Sleep(sleepTimer);
                    sleepTimer += 1000;

                    if (sleepTimer >= 10000)
                        return null;
                }

                return characterContainers;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed when retrieving character nodes");
            }
        }

        private HtmlDocument GetPlayerCollection(Player player)
        {
            var web = new HtmlWeb();
            var playerCollection = web.Load($"{player.SwgohGgUrl}collection/");

            var sleepTimer = 1000;
            while (playerCollection == null || playerCollection.DocumentNode.ChildNodes.Count == 0)
            {
                System.Threading.Thread.Sleep(sleepTimer);
                sleepTimer += 1000;
                playerCollection = web.Load($"{player.SwgohGgUrl}collection/");
                if (sleepTimer >= 10000)
                    return null;
            }
            return playerCollection;
        }
    }
}