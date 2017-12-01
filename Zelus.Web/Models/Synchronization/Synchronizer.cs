using System;
using Ether.Outcomes;
using Zelus.Web.Models.Synchronization.Synchronizers;

namespace Zelus.Web.Models.Synchronization
{
    public class Synchronizer
    {
        private static volatile bool _isRunning = false;

        public IOutcome<string> Execute()
        {
            if (_isRunning)
            {
                return Outcomes.Failure<string>()
                               .WithMessage("Tried to start the swgoh.gg synchronizer, but it's already running.  Exiting.");
            }

            try
            {
                _isRunning = true;
                return DoExecute();
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<string>()
                               .WithMessage("There was an error while executing the swgoh.gg synchronizer.")
                               .FromException(ex);
            }
            finally
            {
                _isRunning = false;
            }
        }

        private IOutcome<string> DoExecute()
        {
            var unitSync = new UnitSynchronizer();
            var unitOutcome = unitSync.Execute();
            if (unitOutcome.Failure)
                return Outcomes.Failure<string>()
                               .WithMessage("The swgoh.gg synchronizer failed while trying to retrieve units.")
                               .WithMessagesFrom(unitOutcome);

            var playerSync = new PlayerSynchronizer();
            var playerOutcome = playerSync.Execute();
            if (playerOutcome.Failure)
                return Outcomes.Failure<string>()
                               .WithMessage("The swgoh.gg synchronizer failed while trying to retrieve players.")
                               .WithMessagesFrom(playerOutcome);

            var collectionSync = new CollectionSynchronizer();
            var collectionOutcome = collectionSync.Execute();
            if (collectionOutcome.Failure)
                return Outcomes.Failure<string>()
                               .WithMessage("The swgoh.gg synchronizer failed while trying to retrieve collections.")
                               .WithMessagesFrom(collectionOutcome);

            return Outcomes.Success<string>()
                .WithMessage("Successfully ran the swgoh.gg synchronizer.");
        }
    }
}