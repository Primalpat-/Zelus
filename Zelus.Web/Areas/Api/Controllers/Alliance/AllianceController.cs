using System.Linq;
using System.Web.Mvc;
using Zelus.Data;
using Zelus.Logic.API.DataSimplification;
using Zelus.Web.Areas.Api.Controllers.Shared;

namespace Zelus.Web.Areas.Api.Controllers.Alliance
{
    public class AllianceController : ApiControllerBase
    {
        public ActionResult Index(int id = default(int))
        {
            if (id == default(int))
                return View();

            return AllianceInfo(id);
        }

        public JsonResult AllianceInfo(int id)
        {
            var db = new ZelusDbContext();

            var alliances = db.Alliances
                              .Where(a => a.Id == id)
                              .Select(AllianceSimplifier.SimpleAlliance)
                              .ToList();

            return BigJson(alliances);
        }

        public JsonResult Guilds(int id)
        {
            var db = new ZelusDbContext();

            var alliances = db.Alliances
                              .Where(a => a.Id == id)
                              .Select(AllianceSimplifier.SimpleAllianceWithGuilds)
                              .ToList();

            return BigJson(alliances);
        }

        public JsonResult Players(int id)
        {
            var db = new ZelusDbContext();

            var alliances = db.Alliances
                              .Where(a => a.Id == id)
                              .Select(AllianceSimplifier.SimpleAllianceWithPlayers)
                              .ToList();

            return BigJson(alliances);
        }

        public JsonResult Collections(int id)
        {
            var db = new ZelusDbContext();

            var alliances = db.Alliances
                              .Where(a => a.Id == id)
                              .Select(AllianceSimplifier.SimpleAllianceWithCollection)
                              .ToList();

            return BigJson(alliances);
        }
    }
}