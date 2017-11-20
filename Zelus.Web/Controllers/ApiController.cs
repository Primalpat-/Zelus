using System.Linq;
using System.Web.Mvc;
using Zelus.Data;

namespace Zelus.Web.Controllers
{
    public class ApiController : Controller
    {
        public JsonResult Guilds(int swgohGgGuildId = 0, int id = 0)
        {
            var _db = new ZelusDbContext();

            if (swgohGgGuildId == 0)
            {
                var guilds = _db.Guilds
                                .Select(g => new { g.Name, GuildId = g.SwgohGgId, Url = g.SwgohGgUrl })
                                .ToList();

                return Json(guilds, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var guilds = _db.Guilds
                                .Where(g => g.SwgohGgId == swgohGgGuildId)
                                .Select(g => new { g.Name, GuildId = g.SwgohGgId, Url = g.SwgohGgUrl })
                                .ToList();

                return Json(guilds, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Players(int swgohGgGuildId, int id = 0)
        {
            var _db = new ZelusDbContext();

            var guild = _db.Guilds
                           .Single(g => g.SwgohGgId == swgohGgGuildId);

            if (id == 0)
            {
                var players = guild.Players
                                   .Select(p => new { GuildId = p.Guild.SwgohGgId, PlayerId = p.Id, p.InGameName, p.SwgohGgName, p.SwgohGgUrl })
                                   .ToList();

                return Json(players, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var player = guild.Players
                                  .Where(p => p.Id == id)
                                  .Select(p => new { GuildId = p.Guild.SwgohGgId, PlayerId = p.Id, p.InGameName, p.SwgohGgName, p.SwgohGgUrl })
                                  .ToList();

                return Json(player, JsonRequestBehavior.AllowGet);
            }
        }
    }
}