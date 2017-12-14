using System.Collections.Generic;
using System.Linq;
using Ether.Outcomes;
using Zelus.Data;
using Zelus.Logic.Services.TerritoryWarPlanning.Strategy;

namespace Zelus.Logic.Services.TerritoryWarPlanning
{
    public class PlanningService
    {
        public IOutcome<StrategyResultViewModel> Execute(PlanningStrategyBase strategy, ZelusDbContext db, Player player)
        {
            var topCharacters = GetTopCharacters(db, player.GuildId, strategy.NumberOfTopCharacters);
            return strategy.Plan(player, topCharacters);
        }

        private List<PlayerCharacterViewModel> GetTopCharacters(ZelusDbContext db, int guildId, int numOfTopCharacters)
        {
            var topPlayerCharacters = db.Guilds
                                        .Where(g => g.Id == guildId)
                                        .SelectMany(g => g.Players)
                                        .SelectMany(p => p.PlayerCharacters)
                                        .OrderByDescending(pc => pc.Power)
                                        .Take(numOfTopCharacters)
                                        .ToList();

            return topPlayerCharacters.Select(GetModelForPlayerCharacter)
                                      .ToList();
        }

        private PlayerCharacterViewModel GetModelForPlayerCharacter(PlayerCharacter pc)
        {
            var model = new PlayerCharacterViewModel();

            model.PlayerId = pc.PlayerId;
            model.Id = pc.Id;
            model.Name = pc.Unit.Name;
            model.Level = pc.Level;
            model.Stars = pc.Stars;
            model.Gear = pc.Gear;
            model.Power = pc.Power;

            return model;
        }
    }
}