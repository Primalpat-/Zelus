using System.Collections.Generic;
using System.Linq;
using Zelus.Data.Models;
using Zelus.Web.Models.Extensions;

namespace Zelus.Web.Models.Factories
{
    public static class LeaderboardModelFactory
    {
        private static ZelusContext _db;

        public static List<LeaderboardVM> GetModel()
        {
            _db = new ZelusContext();

            var models = new List<LeaderboardVM>();

            var raid = _db.Raids.First(r => r.Name == "Tank Takedown (Heroic)");
            var model = new LeaderboardVM();
            model.RaidName = raid.Name;
            model.RaidSubtext = raid.Subtext;
            model.PhaseOneSquads = raid.PhaseOneSquads(10);
            model.PhaseTwoSquads = raid.PhaseTwoSquads(10);
            model.PhaseThreeSquads = raid.PhaseThreeSquads(10);
            model.PhaseFourSquads = raid.PhaseFourSquads(10);
            models.Add(model);

            raid = _db.Raids.First(r => r.Name == "Tank Takedown (Normal)");
            model = new LeaderboardVM();

            model.RaidName = raid.Name;
            model.RaidSubtext = raid.Subtext;
            model.PhaseOneSquads = raid.PhaseOneSquads(10);
            model.PhaseTwoSquads = raid.PhaseTwoSquads(10);
            model.PhaseThreeSquads = raid.PhaseThreeSquads(10);
            model.PhaseFourSquads = raid.PhaseFourSquads(10);
            models.Add(model);

            return models;
        }
    }
}