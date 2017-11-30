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
                                                          .SelectSingleNode("//*[contains(concat(\" \", normalize-space(@class), \" \"), \" collection-char-list \")]")
                                                          .Element("div")                                                //Get the child row
                                                          .Elements("div")                                               //Get the col children
                                                          .Select(e => e.Element("div"))                                 //Select the collection-char
                                                          .Where(d => !d.Attributes["class"].Value.Contains("missing"))  //Filter out the locked characters
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