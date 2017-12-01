using System.Linq;
using System.Web.Mvc;
using Zelus.Data;

namespace Zelus.Web.Controllers
{
    public class ApiController : Controller
    {
        public JsonResult Guilds(int swgohGgGuildId = 0, int id = 0)
        {
            var db = new ZelusDbContext();

            if (swgohGgGuildId == 0)
            {
                var guilds = db.Guilds
                               .Select(g => new { g.Name, GuildId = g.SwgohGgId, Url = g.SwgohGgUrl })
                               .ToList();

                return Json(guilds, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var guilds = db.Guilds
                               .Where(g => g.SwgohGgId == swgohGgGuildId)
                               .Select(g => new { g.Name, GuildId = g.SwgohGgId, Url = g.SwgohGgUrl })
                               .ToList();

                return Json(guilds, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Players(int swgohGgGuildId, int id = 0)
        {
            var db = new ZelusDbContext();

            var guild = db.Guilds
                          .Single(g => g.SwgohGgId == swgohGgGuildId);

            if (id == 0)
            {
                var players = guild.Players
                                   .Select(p => new { GuildId = p.Guild.SwgohGgId, PlayerId = p.Id, p.InGameName, p.SwgohGgName, p.SwgohGgUrl, p.LastSync })
                                   .ToList();

                return Json(players, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var player = guild.Players
                                  .Where(p => p.Id == id)
                                  .Select(p => new { GuildId = p.Guild.SwgohGgId, PlayerId = p.Id, p.InGameName, p.SwgohGgName, p.SwgohGgUrl, p.LastSync })
                                  .ToList();

                return Json(player, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Collection(int swgohGgGuildId, int playerId)
        {
            var db = new ZelusDbContext();

            var units = db.PlayerCharacters
                          .Where(pc => pc.PlayerId == playerId)
                          .Select(pc => new
                          {
                              pc.PlayerId,
                              PlayerSwgohGgName = pc.Player.SwgohGgName,
                              UnitName = pc.Unit.Name,
                              UnitBaseId = pc.Unit.BaseId,
                              UnitMaxPower = pc.Unit.Power,
                              CharacterGear = pc.Gear,
                              CharacterLevel = pc.Level,
                              CharacterStars = pc.Stars,
                              CharacterPower = pc.Power
                          })
                          .ToList();

            return Json(units, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Collections(int swgohGgGuildId)
        {
            var db = new ZelusDbContext();

            var units = db.PlayerCharacters
                          .Where(pc => pc.Player.Guild.SwgohGgId == swgohGgGuildId)
                          .Select(pc => new
                          {
                              pc.PlayerId,
                              PlayerSwgohGgName = pc.Player.SwgohGgName,
                              UnitName = pc.Unit.Name,
                              UnitBaseId = pc.Unit.BaseId,
                              UnitMaxPower = pc.Unit.Power,
                              CharacterGear = pc.Gear,
                              CharacterLevel = pc.Level,
                              CharacterStars = pc.Stars,
                              CharacterPower = pc.Power
                          })
                          .ToList();

            return Json(units, JsonRequestBehavior.AllowGet);
        }
    }
}