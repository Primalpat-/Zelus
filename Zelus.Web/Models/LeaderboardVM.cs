using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zelus.Data.Models;

namespace Zelus.Web.Models
{
    public class LeaderboardVM
    {
        public string RaidName { get; set; }
        public string RaidSubtext { get; set; }
        public List<Squad> PhaseOneSquads { get; set; }
        public List<Squad> PhaseTwoSquads { get; set; }
        public List<Squad> PhaseThreeSquads { get; set; }
        public List<Squad> PhaseFourSquads { get; set; }
    }
}