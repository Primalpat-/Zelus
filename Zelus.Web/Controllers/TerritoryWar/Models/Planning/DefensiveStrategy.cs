using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ether.Outcomes;
using Humanizer;
using Zelus.Data;

namespace Zelus.Web.Models.Views.TerritoryWar.Planning
{
    public class DefensiveStrategy : PlanningStrategyBase
    {
        public override int NumberOfTopCharacters => 1000;

        public override IOutcome<StrategyResultVM> Plan(Player player, List<PlayerCharacterVM> topGuildCharacters)
        {
            var playerTopCharacters = topGuildCharacters.Where(pc => pc.PlayerId == player.Id)
                                                        .ToList();

            var model = new StrategyResultVM();

            model.LastSyncHumanized = player.LastCollectionSync.Humanize();
            model.LastSyncDateTime = player.LastCollectionSync.ToString("r");
            model.NumberOfDefensiveTeams = playerTopCharacters.Count() / 5;
            model.TopCharacters = playerTopCharacters;

            return Outcomes.Success<StrategyResultVM>()
                           .WithValue(model);
        }
    }
}