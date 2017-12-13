using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ether.Outcomes;
using Humanizer;
using Zelus.Data;

namespace Zelus.Web.Models.Views.TerritoryWar.Planning
{
    public class AggressiveStrategy : PlanningStrategyBase
    {
        public override int NumberOfTopCharacters => 3000;

        public override IOutcome<StrategyResultVM> Plan(Player player, List<PlayerCharacterVM> topGuildCharacters)
        {
            var playerTopCharacters = topGuildCharacters.Where(pc => pc.PlayerId == player.Id)
                                            .OrderByDescending(pc => pc.Power)
                                            .ToList();

            var numOfTopCharacters = playerTopCharacters.Count();
            var numOfDefCharacters = numOfTopCharacters / 3;

            var model = new StrategyResultVM();

            model.LastSyncHumanized = player.LastCollectionSync.Humanize();
            model.LastSyncDateTime = player.LastCollectionSync.ToString("r");
            model.NumberOfDefensiveTeams = numOfDefCharacters / 5;
            model.TopCharacters = playerTopCharacters.Take(numOfDefCharacters)
                                                     .ToList();

            return Outcomes.Success<StrategyResultVM>()
                           .WithValue(model);
        }
    }
}