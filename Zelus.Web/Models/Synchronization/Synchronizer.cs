using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ether.Outcomes;
using Z.Core.Extensions;
using Zelus.Data.Models;
using Zelus.Web.Models.Rules;

namespace Zelus.Web.Models.Synchronization
{
    public static class Synchronizer
    {
        public static IOutcome Execute(string guildUrl)
        {
            var db = new ZelusContext();
            var guild = db.Guilds.FirstOrDefault(g => g.Url.ToLower() == guildUrl.ToLower());

            if (guild.IsNotNull() && !SynchronizationRules.IsAllowed(guild))
                return Outcomes.Success<int>()
                               .WithMessage("Guilds are only allowed to sync once every 24 hours.")
                               .WithValue(guild.Id);

            var guildOutcome = GuildSynchronizer.Execute(guild, guildUrl);
            if (guildOutcome.Failure)
                return Outcomes.Failure()
                               .WithMessagesFrom(guildOutcome);

            var playerOutcome = PlayerSynchronizer.Execute(guildOutcome.Value.Id);
            if (playerOutcome.Failure)
                return Outcomes.Failure()
                               .WithMessagesFrom(playerOutcome);

            var playerCharacterOutcome = PlayerCharacterSynchronizer.Execute(guildOutcome.Value.Id);
            if (playerCharacterOutcome.Failure)
                return Outcomes.Failure()
                               .WithMessagesFrom(playerCharacterOutcome);

            return Outcomes.Success();
        }
    }
}