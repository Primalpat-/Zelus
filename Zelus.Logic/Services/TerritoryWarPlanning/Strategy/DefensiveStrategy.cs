using System.Collections.Generic;
using System.Linq;
using Ether.Outcomes;
using Humanizer;
using Zelus.Data;

namespace Zelus.Logic.Services.TerritoryWarPlanning.Strategy
{
    public class DefensiveStrategy : PlanningStrategyBase
    {
        public override int NumberOfTopCharacters => 1000;

        public override IOutcome<StrategyResultViewModel> Plan(Player player, List<PlayerCharacterViewModel> topGuildCharacters)
        {
            var playerTopCharacters = topGuildCharacters.Where(pc => pc.PlayerId == player.Id)
                                                        .ToList();

            var model = new StrategyResultViewModel();

            model.IsVisible = true;
            model.LastSyncHumanized = player.LastCollectionSync.Humanize();
            model.LastSyncDateTime = player.LastCollectionSync.ToString("r");
            model.NumberOfDefensiveTeams = playerTopCharacters.Count() / 5;
            model.TopCharacters = playerTopCharacters;

            return Outcomes.Success<StrategyResultViewModel>()
                           .WithValue(model);
        }
    }
}