using System.Collections.Generic;
using System.Linq;
using Ether.Outcomes;
using Zelus.Data;
using Zelus.Web.Models.Views.TerritoryWar.Planning;

namespace Zelus.Web.Models.Views.TerritoryWar
{
    public class PlanningService
    {
        public IOutcome<StrategyResultVM> Execute(PlanningStrategyBase strategy, ZelusDbContext db, Player player)
        {
            var topCharacters = GetTopCharacters(db, player.GuildId, strategy.NumberOfTopCharacters);
            return strategy.Plan(player, topCharacters);
        }

        private List<PlayerCharacterVM> GetTopCharacters(ZelusDbContext db, int guildId, int numOfTopCharacters)
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

        private PlayerCharacterVM GetModelForPlayerCharacter(PlayerCharacter pc)
        {
            var model = new PlayerCharacterVM();

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