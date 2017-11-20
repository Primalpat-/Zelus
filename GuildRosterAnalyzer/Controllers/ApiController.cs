using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildRosterAnalyzer.Data;

namespace GuildRosterAnalyzer.Web.Controllers
{
    public class ApiController : Controller
    {
        public JsonResult Guilds(int guildId = 0, int id = 0)
        {
            var _db = new GraDbContext();

            if (guildId == 0)
            {
                var guilds = _db.Guilds
                                .Select(g => new { g.Id, g.Name, g.SwgohGgId, g.SwgohGgUrl })
                                .ToList();

                return Json(guilds, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var guilds = _db.Guilds
                                .Where(g => g.Id == guildId)
                                .Select(g => new { g.Id, g.Name, g.SwgohGgId, g.SwgohGgUrl })
                                .ToList();

                return Json(guilds, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Players(int guildId, int id = 0)
        {
            var _db = new GraDbContext();

            return Json("It worked", JsonRequestBehavior.AllowGet);
        }
    }
}