using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ether.Outcomes;
using Humanizer;
using LinqKit;
using Z.Core.Extensions;
using Zelus.Data;
using Zelus.Web.Helpers.Extensions;
using Zelus.Web.Models.Views.Mods;

namespace Zelus.Web.Controllers
{
    public class ModsController : Controller
    {
        public ActionResult Unplanned(int id)
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

        public ActionResult UnplannedMods(int playerId, List<string> sets, string modSlot, int primary, List<int> sorts)
        {
            var db = new ZelusDbContext();

            var query = PlayerModsWithStat.BelongsToPlayer(playerId)
                                 .And(PlayerModsWithStat.IsNotInPlayerSet())
                                 .And(PlayerModsWithStat.IsOfSet(sets))
                                 .And(PlayerModsWithStat.IsOfSlot(modSlot))
                                 .And(PlayerModsWithStat.IsOfPrimary(primary));

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
            if (modIds.IsNull() || modIds.Count != 6)
                return Json(Outcomes.Failure().WithMessage("Can only save 6 mods to a mod set."), JsonRequestBehavior.AllowGet);

            var db = new ZelusDbContext();
            var mods = db.PlayerMods
                         .Where(pm => modIds.Contains(pm.Id))
                         .ToList();

            if (mods.Count(pm => pm.SlotId == (int)ModSlots.Square) != 1 ||
                mods.Count(pm => pm.SlotId == (int)ModSlots.Arrow) != 1 ||
                mods.Count(pm => pm.SlotId == (int)ModSlots.Diamond) != 1 ||
                mods.Count(pm => pm.SlotId == (int)ModSlots.Triangle) != 1 ||
                mods.Count(pm => pm.SlotId == (int)ModSlots.Circle) != 1 ||
                mods.Count(pm => pm.SlotId == (int)ModSlots.Cross) != 1)
                return Json(Outcomes.Failure().WithMessage("Can only save 1 mod per slot."), JsonRequestBehavior.AllowGet);

            var set = new PlayerModSet();

            set.PlayerId = playerId;
            set.Mod1Id = modIds[0];
            set.Mod2Id = modIds[1];
            set.Mod3Id = modIds[2];
            set.Mod4Id = modIds[3];
            set.Mod5Id = modIds[4];
            set.Mod6Id = modIds[5];

            db.PlayerModSets.Add(set);
            db.SaveChanges();

            return Json(Outcomes.Success());
        }

        public JsonResult DeleteModSet(int setId)
        {
            if (setId == 0)
                return Json(Outcomes.Failure().WithMessage("Cannot delete a modset without its id."), JsonRequestBehavior.AllowGet);

            var db = new ZelusDbContext();
            var set = db.PlayerModSets.Single(s => s.Id == setId);
            db.PlayerModSets.Remove(set);
            db.SaveChanges();

            return Json(Outcomes.Success());
        }
    }
}