using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zelus.Data.Models;
using Zelus.Web.Models;
using Zelus.Web.Models.Synchronization;

namespace Zelus.Web.Controllers
{
    public class SquadController : Controller
    {

        [HttpGet]
        public ActionResult Index(int id = default(int))
        {
            if (id == default(int))
                return View("Create");

            var db = new ZelusContext();
            var model = db.Squads.Find(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult SquadDetail(string playerUsername)
        {
            var model = new CreateSquadVM();
            return PartialView("_SquadDetail", model);
        }

        [HttpPost]
        public ActionResult SquadDetail(CreateSquadVM model)
        {
            return RedirectToAction("Index", "Leaderboard");
        }

        [HttpPost]
        public ActionResult SyncPlayerData(string playerUsername)
        {
            var syncOutcome = Synchronizer.ExecuteForPlayer(playerUsername);
            return Json(syncOutcome, JsonRequestBehavior.AllowGet);
        }
    }
}