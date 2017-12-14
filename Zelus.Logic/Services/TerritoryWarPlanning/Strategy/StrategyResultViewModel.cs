using System.Collections.Generic;

namespace Zelus.Logic.Services.TerritoryWarPlanning.Strategy
{
    public class StrategyResultViewModel
    {
        public StrategyResultViewModel()
        {
            TopCharacters = new List<PlayerCharacterViewModel>();
        }

        public bool IsVisible { get; set; }
        public string LastSyncHumanized { get; set; }
        public string LastSyncDateTime { get; set; }
        public int NumberOfDefensiveTeams { get; set; }
        public List<PlayerCharacterViewModel> TopCharacters { get; set; }
    }
}