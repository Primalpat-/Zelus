using System;
using System.Linq;
using Z.Core.Extensions;
using Zelus.Data.Models;

namespace Zelus.Web.Models.Rules
{
    public static class SynchronizationRules
    {
        public static bool IsAllowed(Guild guild)
        {
            if (guild.IsNull())
                return true;

            var lastSynchronization = guild.GuildSynchronizations
                                           .OrderByDescending(s => s.Timestamp)
                                           .FirstOrDefault();

            if (lastSynchronization.IsNull())
                return true;

            return (DateTime.UtcNow - lastSynchronization.Timestamp).TotalHours >= 24;
        }

        public static bool IsAllowed(Player player)
        {
            if (player.IsNull())
                return true;

            var lastSynchronization = player.PlayerSynchronizations
                                            .OrderByDescending(s => s.Timestamp)
                                            .FirstOrDefault();

            if (lastSynchronization.IsNull())
                return true;

            return (DateTime.UtcNow - lastSynchronization.Timestamp).TotalHours >= 24;
        }
    }
}