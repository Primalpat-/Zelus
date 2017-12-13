using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ether.Outcomes;
using Zelus.Data;

namespace Zelus.Web.Models.Views.TerritoryWar.Planning
{
    public abstract class PlanningStrategyBase
    {
        public abstract int NumberOfTopCharacters { get; }
        public abstract IOutcome<StrategyResultVM> Plan(Player player, List<PlayerCharacterVM> topGuildCharacters);
    }
}