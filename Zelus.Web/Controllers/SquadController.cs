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
using Zelus.Web.Models.Extensions;
using Zelus.Web.Models.Synchronization;

namespace Zelus.Web.Controllers
{
    public class SquadController : ZelusControllerBase
    {

        [HttpGet]
        public ActionResult Index(int id = default(int))
        {
            if (id == default(int))
                return View("Create");
            
            var model = Db.Squads.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult SyncPlayerData(string playerUsername)
        {
            var syncOutcome = Synchronizer.ExecuteForPlayer(playerUsername);
            return Json(syncOutcome, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SquadDetail(string playerUsername)
        {
            var model = new CreateSquadVM();
            model.PlayerUsername = playerUsername;

            return PartialView("_SquadDetail", model);
        }

        [HttpPost]
        public ActionResult CreateSquad(CreateSquadVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Index");

                Log.Information("Using {@model} to create squad", model);

                var squad = new Squad();

                squad.PlayerId = Db.PlayerCharacters.Single(pc => pc.Id == model.Member1Id).PlayerId;
                squad.Name = model.Name;
                squad.TargetPhaseId = model.PhaseId;
                squad.Damage = model.Damage;
                squad.VictoryScreenImageId = model.VictoryScreenImageId;

                squad.Member1Id = CreateSquadCharacter(model.Member1Id).ToInt32();
                squad.Member2Id = CreateSquadCharacter(model.Member2Id);
                squad.Member3Id = CreateSquadCharacter(model.Member3Id);
                squad.Member4Id = CreateSquadCharacter(model.Member4Id);
                squad.Member5Id = CreateSquadCharacter(model.Member5Id);

                squad.Notes = model.Notes;
                squad.Timestamp = DateTime.UtcNow;

                Db.Squads.Add(squad);
                Db.SaveChanges();

                Log.Information("Created {@squad}", squad);

                return RedirectToAction("Index", "Squad", new { id = squad.Id });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed creating squad with {@model}", model);
                return View("Error");
            }
        }

        public ActionResult GetPlayerCharacterPortrait(int playerCharacterId)
        {
            var db = new ZelusContext();
            var model = db.PlayerCharacters
                          .Find(playerCharacterId)
                          .ToCharacterVM();
            return PartialView("_PlayerCharacter", model);
        }

        #region "Helpers"

        private int? CreateSquadCharacter(int? playerCharacterId)
        {
            if (!playerCharacterId.HasValue)
                return null;

            var squadCharacter = Db.PlayerCharacters
                                   .Find(playerCharacterId)
                                   .ToSquadCharacter();
            Db.SquadCharacters.Add(squadCharacter);
            Db.SaveChanges();

            return squadCharacter.Id;
        }

        #endregion

        #region "Victory Screen Upload"

        public ActionResult UploadImage(HttpPostedFileBase VictoryScreenFile)
        {
            if (VictoryScreenFile.IsNull())
                return Content("");
            
            var fileName = Path.GetFileName(VictoryScreenFile.FileName);
            var guid = Guid.NewGuid();
            var physicalPath = Path.Combine(Server.MapPath("~/Content/VictoryScreens"), $"{guid:N}-{fileName}");
            VictoryScreenFile.SaveAs(physicalPath);

            var db = new ZelusContext();
            var victoryScreen = new VictoryScreenImage{ Path = physicalPath };

            //var target = new MemoryStream();
            //VictoryScreenFile.InputStream.CopyTo(target);
            //victoryScreen.Data = target.ToArray();

            db.VictoryScreenImages.Add(victoryScreen);
            db.SaveChanges();

            return Content(victoryScreen.Id.ToString());
        }
        public ActionResult DeleteImage(string fileNames, int victoryScreenImageId)
        {
            if (victoryScreenImageId.IsDefault())
                return Content("");

            var db = new ZelusContext();
            var image = db.VictoryScreenImages.Find(victoryScreenImageId);

            if (image.IsNull() || !System.IO.File.Exists(image.Path))
                return Content("");
            
            System.IO.File.Delete(image.Path);
            db.VictoryScreenImages.Remove(image);
            db.SaveChanges();

            return Content("");
        }

        #endregion
    }
}