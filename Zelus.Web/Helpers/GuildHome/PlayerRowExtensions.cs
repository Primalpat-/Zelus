using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using Zelus.Data;

namespace Zelus.Web.Helpers.GuildHome
{
    public static class PlayerRowExtensions
    {
        public static Player ParsePlayer(this IEnumerable<HtmlNode> data, int guildId)
        {
            var player = new Player();

            player.GuildId = guildId;
            player.InGameName = data.ParsePlayerInGameName();
            player.SwgohGgName = data.ParsePlayerSwgohGgName();
            player.SwgohGgUrl = data.ParsePlayerSwgohGgUrl();
            
            return player;
        }

        private static string ParsePlayerInGameName(this IEnumerable<HtmlNode> data)
        {
            return HttpUtility.HtmlDecode(data.ElementAt(0).InnerText.Replace("\n", ""));
        }

        private static string ParsePlayerSwgohGgUrl(this IEnumerable<HtmlNode> data)
        {
            var userName = ParsePlayerSwgohGgName(data);
            return $"https://swgoh.gg/u/{userName}/";
        }

        private static string ParsePlayerSwgohGgName(this IEnumerable<HtmlNode> data)
        {
            var userLink = data.ElementAt(0).ChildNodes[1].Attributes["href"].Value;
            var userName = userLink.Split('/')[2];
            return userName.Trim();
        }
    }
}