using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zelus.Data;

namespace Zelus.Web.Models.DataSimplification
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