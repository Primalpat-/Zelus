using System.Linq;
using System.Web.Mvc;
using Zelus.Data;
using Zelus.Web.Models.DataSimplification;

namespace Zelus.Web.Areas.Api.Controllers
{
    public class GuildController : ApiControllerBase
    {
        public ActionResult Index(int id = default(int))
        {
            if (id == default(int))
                return View();

            return GuildInfo(id);
        }

        public JsonResult GuildInfo(int id)
        {
            var db = new ZelusDbContext();

            var guilds = db.Guilds
                           .Where(g => g.SwgohGgId == id)
                           .Select(GuildSimplifier.SimpleGuild)
                           .ToList();

            return BigJson(guilds);
        }

        public JsonResult Players(int id)
        {
            var db = new ZelusDbContext();

            var guilds = db.Guilds
                           .Where(g => g.SwgohGgId == id)
                           .Select(GuildSimplifier.SimpleGuildWithPlayers)
                           .ToList();

            return BigJson(guilds);
        }

        public JsonResult Collections(int id)
        {
            var db = new ZelusDbContext();

            var guilds = db.Guilds
                           .Where(g => g.SwgohGgId == id)
                           .Select(GuildSimplifier.SimpleGuildWithCollections)
                           .ToList();

            return BigJson(guilds);
        }
    }
}