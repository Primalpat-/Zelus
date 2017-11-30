using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data;
using Zelus.Web.Helpers.PlayerCollection;

namespace Zelus.Web.Models.Scrapers
{
    public class CollectionScraper
    {
        public List<PlayerCharacter> Execute(ZelusDbContext db)
        {
            var results = new List<PlayerCharacter>();
            var players = db.Players.ToList();

            foreach (var player in players)
            {
                var characterContainers = GetCharacterNodes(player);

                foreach (var container in characterContainers)
                {
                    var character = container.ParseCharacter(db, player.Id);
                    results.Add(character);
                }
            }

            return results;
        }

        private List<HtmlNode> GetCharacterNodes(Player player)
        {
            try
            {
                var web = new HtmlWeb();
                var characterContainers = new List<HtmlNode>();
                var sleepTimer = 1000;
                while (characterContainers.Count == 0)
                {
                    var playerCollection = GetPlayerCollection(player);
                    characterContainers = playerCollection.DocumentNode?
                                                          .SelectSingleNode("//*[contains(concat(\" \", normalize-space(@class), \" \"), \" collection-char-list \")]")?
                                                          .Element("div")?                                                //Get the child row
                                                          .Elements("div")?                                               //Get the col children
                                                          .Select(e => e.Element("div"))?                                 //Select the collection-char
                                                          .Where(d => !d.Attributes["class"].Value.Contains("missing"))?  //Filter out the locked characters
                                                          .ToList();

                    System.Threading.Thread.Sleep(sleepTimer);
                    if (sleepTimer <= 30000)
                        sleepTimer += 1000;
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
                playerCollection = web.Load($"{player.SwgohGgUrl}collection/");
                if (sleepTimer <= 30000)
                    sleepTimer += 1000;
            }
            return playerCollection;
        }
    }
}