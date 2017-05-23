using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Z.Core.Extensions;
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

        [HttpPost]
        public ActionResult SquadDetail(string playerUsername)
        {
            var db = new ZelusContext();
            var model = new CreateSquadVM();
            model.PlayerUsername = playerUsername;
            //model.PlayerCharacters = db.PlayerCharacters
            //                           .Where(pc => pc.Player.Name.ToLower() == playerUsername.ToLower())
            //                           .ToList();
            return PartialView("_SquadDetail", model);
        }

        [HttpPost]
        public ActionResult CreateSquad(CreateSquadVM model)
        {
            if (!ModelState.IsValid)
                return View("Index");

            var db = new ZelusContext();
            var squad = new Squad();
            squad.Name = model.Name;
            squad.TargetPhaseId = model.PhaseId;
            squad.Damage = model.Damage;
            squad.Notes = model.Notes;
            squad.Member1Id = model.Member1Id;
            squad.Timestamp = DateTime.UtcNow;

            if (!model.Member2Id.IsDefault())
                squad.Member2Id = model.Member2Id;

            if (!model.Member3Id.IsDefault())
                squad.Member3Id = model.Member3Id;

            if (!model.Member4Id.IsDefault())
                squad.Member4Id = model.Member4Id;

            if (!model.Member5Id.IsDefault())
                squad.Member5Id = model.Member5Id;

            db.Squads.Add(squad);
            db.SaveChanges();

            return RedirectToAction("Index", "Leaderboard");
        }

        [HttpPost]
        public ActionResult SyncPlayerData(string playerUsername)
        {
            var syncOutcome = Synchronizer.ExecuteForPlayer(playerUsername);
            return Json(syncOutcome, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPlayerCharacterPortrait(int playerCharacterId)
        {
            var db = new ZelusContext();
            var model = db.PlayerCharacters.Find(playerCharacterId);
            return PartialView("_PlayerCharacter", model);
        }

        #region "Victory Screen Upload"

        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file.IsNull())
                return Content("");

            var db = new ZelusContext();
            var victoryScreen = new VictoryScreenImage();

            var target = new MemoryStream();
            file.InputStream.CopyTo(target);
            victoryScreen.Data = target.ToArray();

            db.VictoryScreenImages.Add(victoryScreen);
            db.SaveChanges();
            
            return Content("");
        }
    }

        //public ActionResult DeleteImage()
        //{
            
        //}

        #endregion
}