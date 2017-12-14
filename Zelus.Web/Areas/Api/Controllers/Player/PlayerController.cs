using System.Linq;
using System.Web.Mvc;
using Zelus.Data;
using Zelus.Logic.API.DataSimplification;
using Zelus.Web.Areas.Api.Controllers.Shared;

namespace Zelus.Web.Areas.Api.Controllers.Player
{
    public class PlayerController : ApiControllerBase
    {
        public ActionResult Index(int id = default(int))
        {
            if (id == default(int))
                return View();

            return PlayerInfo(id);
        }

        public ActionResult PlayerInfo(int id)
        {
            var db = new ZelusDbContext();

            var players = db.Players
                            .Where(p => p.Id == id)
                            .Select(PlayerSimplifier.SimplePlayer)
                            .ToList();

            return BigJson(players);
        }

        public ActionResult Collection(int id)
        {
            var db = new ZelusDbContext();

            var players = db.Players
                            .Where(p => p.Id == id)
                            .Select(PlayerSimplifier.SimplePlayerWithCollection)
                            .ToList();

            return BigJson(players);
        }
    }
}