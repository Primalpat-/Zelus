using System.Linq;
using Zelus.Data;

namespace Zelus.Logic.API.DataSimplification
{
    public static class GuildSimplifier
    {
        public static object SimpleGuild(this Guild guild)
        {
            return new
            {
                guild.Name,
                GuildId = guild.SwgohGgId,
                Url = guild.SwgohGgUrl
            };
        }

        public static object SimpleGuildWithPlayers(this Guild guild)
        {
            return new
            {
                guild.Name,
                GuildId = guild.SwgohGgId,
                Url = guild.SwgohGgUrl,
                Players = guild.Players
                               .Select(PlayerSimplifier.SimplePlayer)
                               .ToList()
            };
        }

        public static object SimpleGuildWithCollections(this Guild guild)
        {
            return new
            {
                guild.Name,
                GuildId = guild.SwgohGgId,
                Url = guild.SwgohGgUrl,
                Players = guild.Players
                    .Select(PlayerSimplifier.SimplePlayerWithCollection)
                    .ToList()
            };
        }
    }
}