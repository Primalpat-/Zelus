using System.Collections.Generic;
using System.Linq;
using Ether.Outcomes;
using Humanizer;
using Zelus.Data;

namespace Zelus.Logic.Services.TerritoryWarPlanning.Strategy
{
    public class AggressiveStrategy : PlanningStrategyBase
    {
        public override int NumberOfTopCharacters => 3000;

        public override IOutcome<StrategyResultViewModel> Plan(Player player, List<PlayerCharacterViewModel> topGuildCharacters)
        {
            var playerTopCharacters = topGuildCharacters.Where(pc => pc.PlayerId == player.Id)
                                            .OrderByDescending(pc => pc.Power)
                                            .ToList();

            var numOfTopCharacters = playerTopCharacters.Count();
            var numOfDefCharacters = numOfTopCharacters / 3;

            var model = new StrategyResultViewModel();

            model.IsVisible = true;
            model.LastSyncHumanized = player.LastCollectionSync.Humanize();
            model.LastSyncDateTime = player.LastCollectionSync.ToString("r");
            model.NumberOfDefensiveTeams = numOfDefCharacters / 5;
            model.TopCharacters = playerTopCharacters.Take(numOfDefCharacters)
                                                     .ToList();

            return Outcomes.Success<StrategyResultViewModel>()
                           .WithValue(model);
        }
    }
}