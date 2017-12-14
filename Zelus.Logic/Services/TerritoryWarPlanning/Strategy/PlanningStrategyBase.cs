using System.Collections.Generic;
using Ether.Outcomes;
using Zelus.Data;

namespace Zelus.Logic.Services.TerritoryWarPlanning.Strategy
{
    public abstract class PlanningStrategyBase
    {
        public abstract int NumberOfTopCharacters { get; }
        public abstract IOutcome<StrategyResultViewModel> Plan(Player player, List<PlayerCharacterViewModel> topGuildCharacters);
    }
}