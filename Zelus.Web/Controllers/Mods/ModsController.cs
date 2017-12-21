using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Ether.Outcomes;
using Humanizer;
using LinqKit;
using Z.Core.Extensions;
using Zelus.Data;
using Zelus.Logic.Extensions.Entities.PlayerModsWithStats;
using Zelus.Web.Controllers.Mods.Models;

namespace Zelus.Web.Controllers.Mods
{
    public class ModsController : Controller
    {
        public ActionResult Unplanned(int id)
        {
            var db = new ZelusDbContext();
            var player = db.Players.FirstOrDefault(p => p.Id == id);

            if (player.IsNull())
                return View("Error");

            var mods = db.PlayerModsWithStats
                         .Where(PlayerModsWithStat.BelongsToPlayer(player.Id))
                         .ToList()
                         .Where(pms => pms.SlotId == 2 || pms.SlotId == 4)
                         .ToList();

            var modVMs = ModVMFactory.GetModVMs(mods, true);

            var model = new ModPlannerVM
            {
                PlayerId = player.Id,
                LastSyncDateTime = player.LastModSync,
                LastSyncHumanized = player.LastModSync.Humanize(),
                Mods = modVMs
            };

            return View(model);
        }

        public ActionResult Planned(int id)
        {
            var db = new ZelusDbContext();
            var player = db.Players.FirstOrDefault(p => p.Id == id);

            if (player.IsNull())
                return View("Error");

            var model = new ModPlannerVM();
            model.PlayerId = player.Id;
            model.LastSyncDateTime = player.LastModSync;
            model.LastSyncHumanized = player.LastModSync.Humanize();

            return View(model);
        }

        public ActionResult UnplannedMods(int playerId, List<string> sets, string modSlot, int primary, List<int> sorts, bool includeModsInSet = false)
        {
            var db = new ZelusDbContext();

            var query = PlayerModsWithStat.BelongsToPlayer(playerId)
                                 .And(PlayerModsWithStat.IsOfSet(sets))
                                 .And(PlayerModsWithStat.IsOfSlot(modSlot))
                                 .And(PlayerModsWithStat.IsOfPrimary(primary));

            if (!includeModsInSet)
                query = query.And(PlayerModsWithStat.IsNotInPlayerSet());

            var mods = db.PlayerModsWithStats
                         .Where(query)
                         .ApplySorts(sorts)
                         .Take(10)
                         .ToList();

            var models = ModVMFactory.GetModVMs(mods, true);

            return PartialView("_SingleColumnMods", models);
        }

        public ActionResult PlannedMods(int playerId, List<string> setNames)
        {
            var db = new ZelusDbContext();

            var query = PlayerModSet.BelongsToPlayer(playerId)
                                    .And(PlayerModSet.HasSetBonus(setNames));

            var sets = db.PlayerModSets
                         .Where(query)
                         .Take(10)
                         .ToList();

            var models = ModVMFactory.GetSetVMs(sets);

            return PartialView("_DoubleColumnMods", models);
        }

        public JsonResult SaveModSet(int playerId, List<int> modIds)
        {
            var db = new ZelusDbContext();

            //Ensure that what was passed is what we expect
            var modsOutcome = PlayerModSetManager.GetModsFromIdList(db, modIds);
            if (modsOutcome.Failure)
                return Json(modsOutcome, JsonRequestBehavior.AllowGet);

            //Remove any previous modsets these mods were a part of
            foreach (var mod in modsOutcome.Value)
                PlayerModSetManager.RemoveModFromAllSets(db, mod);

            var set = new PlayerModSet
            {
                PlayerId = playerId,
                Mod1Id = modIds[0],
                Mod2Id = modIds[1],
                Mod3Id = modIds[2],
                Mod4Id = modIds[3],
                Mod5Id = modIds[4],
                Mod6Id = modIds[5]
            };

            db.PlayerModSets.Add(set);
            db.SaveChanges();

            return Json(Outcomes.Success(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteModSet(int setId)
        {
            if (setId == 0)
                return Json(Outcomes.Failure().WithMessage("Cannot delete a modset without its id."), JsonRequestBehavior.AllowGet);

            var db = new ZelusDbContext();
            var set = db.PlayerModSets.Single(s => s.Id == setId);
            db.PlayerModSets.Remove(set);
            db.SaveChanges();

            return Json(Outcomes.Success(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAllMods(int id)
        {
            var db = new ZelusDbContext();

            var sets = db.PlayerModSets
                .Where(m => m.PlayerId == id)
                .ToList();

            db.PlayerModSets.RemoveRange(sets);
            db.SaveChanges();

            var mods = db.PlayerMods
                         .Where(m => m.PlayerId == id)
                         .ToList();

            db.PlayerMods.RemoveRange(mods);
            db.SaveChanges();

            return Json(Outcomes.Success(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAllModSets(int id)
        {
            var db = new ZelusDbContext();

            var sets = db.PlayerModSets
                         .Where(m => m.PlayerId == id)
                         .ToList();

            db.PlayerModSets.RemoveRange(sets);
            db.SaveChanges();

            return Json(Outcomes.Success(), JsonRequestBehavior.AllowGet);
        }
    }
}