using System.Collections.Generic;

namespace Zelus.Web.Models
{
    public class LeaderboardVM
    {
        public int RaidId { get; set; }
        public string RaidName { get; set; }
        public string RaidSubtext { get; set; }
        public int Phase1Id { get; set; }
        public List<SquadVM> PhaseOneSquads { get; set; }
        public int Phase2Id { get; set; }
        public List<SquadVM> PhaseTwoSquads { get; set; }
        public int Phase3Id { get; set; }
        public List<SquadVM> PhaseThreeSquads { get; set; }
        public int Phase4Id { get; set; }
        public List<SquadVM> PhaseFourSquads { get; set; }
    }
}