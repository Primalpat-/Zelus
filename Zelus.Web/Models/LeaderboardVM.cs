using System.Collections.Generic;

namespace Zelus.Web.Models
{
    public class LeaderboardVM
    {
        public string RaidName { get; set; }
        public string RaidSubtext { get; set; }
        public List<SquadVM> PhaseOneSquads { get; set; }
        public List<SquadVM> PhaseTwoSquads { get; set; }
        public List<SquadVM> PhaseThreeSquads { get; set; }
        public List<SquadVM> PhaseFourSquads { get; set; }
    }
}