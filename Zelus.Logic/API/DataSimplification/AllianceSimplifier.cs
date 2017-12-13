using System.Linq;
using Zelus.Data;

namespace Zelus.Logic.API.DataSimplification
{
    public static class AllianceSimplifier
    {
        public static object SimpleAlliance(this Alliance alliance)
        {
            return new
            {
                alliance.Id,
                alliance.Name,
                NumberOfGuilds = alliance.Guilds.Count
            };
        }

        public static object SimpleAllianceWithGuilds(this Alliance alliance)
        {
            return new
            {
                alliance.Id,
                alliance.Name,
                Guilds = alliance.Guilds
                                 .Select(GuildSimplifier.SimpleGuild)
                                 .ToList()
            };
        }

        public static object SimpleAllianceWithPlayers(this Alliance alliance)
        {
            return new
            {
                alliance.Id,
                alliance.Name,
                Guilds = alliance.Guilds
                                 .Select(GuildSimplifier.SimpleGuildWithPlayers)
                                 .ToList()
            };
        }

        public static object SimpleAllianceWithCollection(this Alliance alliance)
        {
            return new
            {
                alliance.Id,
                alliance.Name,
                Guilds = alliance.Guilds
                    .Select(GuildSimplifier.SimpleGuildWithCollections)
                    .ToList()
            };
        }
    }
}