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

            var raids = _db.Raids
                           .OrderBy(r => r.SortOrder)
                           .ToList();

            var models = new List<LeaderboardVM>();
            foreach(var raid in raids)
                models.Add(GetRaidModel(raid));

            return models;
        }

        private static LeaderboardVM GetRaidModel(Raid raid)
        {
            var model = new LeaderboardVM();

            model.RaidName = raid.Name;
            model.RaidSubtext = raid.Subtext;

            model.PhaseOneSquads = raid.PhaseOneSquads(10)
                                       .Select(GetSquadModel)
                                       .ToList();

            model.PhaseTwoSquads = raid.PhaseTwoSquads(10)
                                       .Select(GetSquadModel)
                                       .ToList();

            model.PhaseThreeSquads = raid.PhaseThreeSquads(10)
                                         .Select(GetSquadModel)
                                         .ToList();

            model.PhaseFourSquads = raid.PhaseFourSquads(10)
                                        .Select(GetSquadModel)
                                        .ToList();

            return model;
        }

        private static SquadVM GetSquadModel(Squad squad, int index)
        {
            var model = new SquadVM();

            model.Id = squad.Id;
            model.SquadName = squad.Name;
            model.PlayerName = squad.Player.Name;
            model.Rank = index + 1;
            model.Damage = squad.Damage;
            model.PhaseHealth = squad.RaidPhas.Health;

            return model;
        }
    }
}