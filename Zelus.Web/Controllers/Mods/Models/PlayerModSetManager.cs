using System.Collections.Generic;
using System.Linq;
using Ether.Outcomes;
using Z.Core.Extensions;
using Zelus.Data;

namespace Zelus.Web.Models.Views.Mods
{
    public static class PlayerModSetManager
    {
        public static IOutcome<List<PlayerMod>> GetModsFromIdList(ZelusDbContext db, List<int> modIds)
        {
            //Only allow saving of exactly 6 mods for a modset
            if (modIds.IsNull() || modIds.Count != 6)
                return Outcomes.Failure<List<PlayerMod>>()
                               .WithMessage("Can only save 6 mods to a mod set.");

            var mods = db.PlayerMods
                         .Where(pm => modIds.Contains(pm.Id))
                         .ToList();

            //Only allow 1 of each slot of mods
            if (mods.Count(pm => pm.SlotId == (int)ModSlots.Square) != 1 ||
                mods.Count(pm => pm.SlotId == (int)ModSlots.Arrow) != 1 ||
                mods.Count(pm => pm.SlotId == (int)ModSlots.Diamond) != 1 ||
                mods.Count(pm => pm.SlotId == (int)ModSlots.Triangle) != 1 ||
                mods.Count(pm => pm.SlotId == (int)ModSlots.Circle) != 1 ||
                mods.Count(pm => pm.SlotId == (int)ModSlots.Cross) != 1)
                return Outcomes.Failure<List<PlayerMod>>()
                               .WithMessage("Can only save 1 mod per slot.");

            return Outcomes.Success<List<PlayerMod>>()
                           .WithValue(mods);
        }

        public static void RemoveModFromAllSets(ZelusDbContext db, PlayerMod mod)
        {
            if (mod.Mod1.Any())
                db.PlayerModSets.RemoveRange(mod.Mod1);
            
            if (mod.Mod2.Any())
                db.PlayerModSets.RemoveRange(mod.Mod2);
            
            if (mod.Mod3.Any())
                db.PlayerModSets.RemoveRange(mod.Mod3);
            
            if (mod.Mod4.Any())
                db.PlayerModSets.RemoveRange(mod.Mod4);
            
            if (mod.Mod5.Any())
                db.PlayerModSets.RemoveRange(mod.Mod5);
            
            if (mod.Mod6.Any())
                db.PlayerModSets.RemoveRange(mod.Mod6);

            db.SaveChanges();
        }
    }
}