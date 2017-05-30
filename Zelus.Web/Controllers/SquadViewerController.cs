using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Z.Core.Extensions;
using Zelus.Data.Models;
using Zelus.Web.Models;
using Zelus.Web.Models.Extensions;

namespace Zelus.Web.Controllers
{
    public class SquadViewerController : Controller
    {
        [HttpGet]
        public ActionResult Index(int raidId = 0, int phaseId = 0)
        {
            var db = new ZelusContext();
            var model = new SquadViewerVM();
            model.RaidId = raidId;
            model.PhaseId = phaseId;

            if (raidId > 0)
                model.RaidName = db.Raids.Single(r => r.Id == raidId).Name;

            if (phaseId > 0)
                model.PhaseName = db.RaidPhases.Single(p => p.Id == phaseId).Name;

            return View(model);
        }

        public ActionResult GetSquads([DataSourceRequest] DataSourceRequest request)
        {
            var db = new ZelusContext();
            var squads = db.Squads
                           .AsQueryable()
                           .Select(s => new
                           {
                               s.Id,
                               s.Name,
                               s.Damage,
                               s.Timestamp,
                               s.Member1Id,
                               s.Member2Id,
                               s.Member3Id,
                               s.Member4Id,
                               s.Member5Id,
                               Raid = new { s.RaidPhas.Raid.Name },
                               Phase = new { s.RaidPhas.Name, s.RaidPhas.Health },
                               Guild = new { s.Player.Guild.Name, s.Player.Guild.Url },
                               Player = new { s.Player.Name, s.Player.CollectionUrl }
                           });
            var result = squads.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSquadCharacter(int squadCharacterId)
        {
            var db = new ZelusContext();
            var model = db.SquadCharacters
                          .Single(sc => sc.Id == squadCharacterId)
                          .ToCharacterVM();
            return PartialView("_PlayerCharacter", model);
        }
    }
}


//Member1 = new
//                                   {
//                                       s.SquadCharacter.Character.Name,
//                                       s.SquadCharacter.Character.Portrait,
//                                       s.SquadCharacter.CharacterLevel,
//                                       s.SquadCharacter.GearLevel,
//                                       s.SquadCharacter.StarLevel,
//                                       s.SquadCharacter.NumberOfZetas,
//                                       s.SquadCharacter.CharacterUrl
//                                   },
//                                   Member2 = new
//                                   {
//                                       s.SquadCharacter1.Character.Name,
//                                       s.SquadCharacter1.Character.Portrait,
//                                       s.SquadCharacter1.CharacterLevel,
//                                       s.SquadCharacter1.GearLevel,
//                                       s.SquadCharacter1.StarLevel,
//                                       s.SquadCharacter1.NumberOfZetas,
//                                       s.SquadCharacter1.CharacterUrl
//                                   },
//                                   Member3 = new
//                                   {
//                                       s.SquadCharacter2.Character.Name,
//                                       s.SquadCharacter2.Character.Portrait,
//                                       s.SquadCharacter2.CharacterLevel,
//                                       s.SquadCharacter2.GearLevel,
//                                       s.SquadCharacter2.StarLevel,
//                                       s.SquadCharacter2.NumberOfZetas,
//                                       s.SquadCharacter2.CharacterUrl
//                                   },
//                                   Member4 = new
//                                   {
//                                       s.SquadCharacter3.Character.Name,
//                                       s.SquadCharacter3.Character.Portrait,
//                                       s.SquadCharacter3.CharacterLevel,
//                                       s.SquadCharacter3.GearLevel,
//                                       s.SquadCharacter3.StarLevel,
//                                       s.SquadCharacter3.NumberOfZetas,
//                                       s.SquadCharacter3.CharacterUrl
//                                   },
//                                   Member5 = new
//                                   {
//                                       s.SquadCharacter4.Character.Name,
//                                       s.SquadCharacter4.Character.Portrait,
//                                       s.SquadCharacter4.CharacterLevel,
//                                       s.SquadCharacter4.GearLevel,
//                                       s.SquadCharacter4.StarLevel,
//                                       s.SquadCharacter4.NumberOfZetas,
//                                       s.SquadCharacter4.CharacterUrl
//                                   }
