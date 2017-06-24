using System.Linq;
using Ether.Outcomes;
using Z.Core.Extensions;
using Zelus.Data.Models;
using Zelus.Web.Models.Extensions;
using Zelus.Web.Models.Rules;

namespace Zelus.Web.Models.Synchronization
{
    public static class Synchronizer
    {
        public static IOutcome ExecuteForGuild(string guildUrl)
        {
            var db = new ZelusContext();
            var guild = db.Guilds.FirstOrDefault(g => g.Url.ToLower() == guildUrl.ToLower());

            if (guild.IsNotNull() && !SynchronizationRules.IsAllowed(guild))
                return Outcomes.Success<int>()
                               .WithMessage("Guilds are only allowed to sync once every 24 hours.");

            var guildOutcome = GuildSynchronizer.Execute(guild, guildUrl);
            if (guildOutcome.Failure)
                return Outcomes.Failure()
                               .WithMessagesFrom(guildOutcome);
            
            var playerCharacterOutcome = PlayerCharacterSynchronizer.Execute(guildOutcome.Value.Id);
            if (playerCharacterOutcome.Failure)
                return Outcomes.Failure()
                               .WithMessagesFrom(playerCharacterOutcome);

            return Outcomes.Success();
        }

        public static IOutcome ExecuteForPlayer(string playerUsername)
        {
            var db = new ZelusContext();
            var collectionUrl = playerUsername.ToCollectionUrl();
            var player = db.Players.FirstOrDefault(p => p.CollectionUrl.ToLower() == collectionUrl.ToLower());

            if (player.IsNotNull() && !SynchronizationRules.IsAllowed(player))
                return Outcomes.Success<int>()
                               .WithMessage("Players are only allowed to sync once every 24 hours.");

            var playerOutcome = PlayerSynchronizer.Execute(player, playerUsername);
            if (playerOutcome.Failure)
                return Outcomes.Failure()
                               .WithMessagesFrom(playerOutcome);

            var playerCharacterOutcome = PlayerCharacterSynchronizer.Execute(playerOutcome.Value.Player.Id, playerOutcome.Value.Document);
            if (playerCharacterOutcome.Failure)
                return Outcomes.Failure()
                               .WithMessagesFrom(playerCharacterOutcome);

            return Outcomes.Success();
        }
    }
}