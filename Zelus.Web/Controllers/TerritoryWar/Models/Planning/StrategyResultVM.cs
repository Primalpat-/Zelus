using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zelus.Web.Models.Views.TerritoryWar.Planning
{
    public class StrategyResultVM
    {
        public StrategyResultVM()
        {
            TopCharacters = new List<PlayerCharacterVM>();
        }

        public string LastSyncHumanized { get; set; }
        public string LastSyncDateTime { get; set; }
        public int NumberOfDefensiveTeams { get; set; }
        public List<PlayerCharacterVM> TopCharacters { get; set; }
    }
}