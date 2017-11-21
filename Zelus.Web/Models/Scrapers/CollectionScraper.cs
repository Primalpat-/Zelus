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
            var web = new HtmlWeb();
            var players = db.Players.ToList();

            foreach (var player in players)
            {
                var playerCollection = web.Load($"{player.SwgohGgUrl}collection/");
                var characterContainers = playerCollection.DocumentNode
                                                          .Descendants("div")
                                                          .Where(d => d.Attributes["class"].IsNotNull() &&
                                                                      d.Attributes["class"].Value.Contains("collection-char"))
                                                          .ToList();

                foreach (var container in characterContainers)
                {
                    var character = container.ParseCharacter(db, player.Id);
                    results.Add(character);
                }
            }

            return results;
        }
    }
}