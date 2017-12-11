using System;
using System.Collections.Generic;
using System.Linq;
using Z.Core.Extensions;
using Zelus.Data;
using Zelus.Web.Helpers.Extensions;

namespace Zelus.Web.Models.Views.Mods
{
    public static class ModVMFactory
    {
        public static List<ModSetVM> GetSetVMs(List<PlayerModSet> sets, bool showCheckbox = false)
        {
            var result = new List<ModSetVM>();

            foreach (var set in sets)
            {
                var mods = new List<PlayerMod>() { set.Mod1, set.Mod2, set.Mod3, set.Mod4, set.Mod5, set.Mod6 };
                var model = new ModSetVM();

                model.Id = set.Id;

                model.Square = GetModVM(mods.Single(m => m.SlotId == (int)ModSlots.Square), showCheckbox);
                model.Arrow = GetModVM(mods.Single(m => m.SlotId == (int) ModSlots.Arrow), showCheckbox);
                model.Diamond = GetModVM(mods.Single(m => m.SlotId == (int)ModSlots.Diamond), showCheckbox);
                model.Triangle = GetModVM(mods.Single(m => m.SlotId == (int)ModSlots.Triangle), showCheckbox);
                model.Circle = GetModVM(mods.Single(m => m.SlotId == (int)ModSlots.Circle), showCheckbox);
                model.Cross = GetModVM(mods.Single(m => m.SlotId == (int)ModSlots.Cross), showCheckbox);

                model.TotalSpeed = $"+{set.Speed()} Speed";
                model.TotalCritChance = $"+{set.CritChance()}% Critical Chance";
                model.TotalCritDamage = $"+{set.CritDamage()}% Critical Damage";
                model.TotalFlatOffense = $"+{set.FlatOffense()} Offense";
                model.TotalPercentageOffense = $"+{set.PercentageOffense()}% Offense";
                model.TotalPotency = $"+{set.Potency()}% Potency";
                model.TotalAccuracy = $"+{set.Accuracy()}% Accuracy";

                model.TotalFlatProtection = $"+{set.FlatProtection()} Protection";
                model.TotalPercentageProtection = $"+{set.PercentageProtection()}% Protection";
                model.TotalFlatHealth = $"+{set.FlatHealth()} Health";
                model.TotalPercentageHealth = $"+{set.PercentageHealth()}% Health";
                model.TotalFlatDefense = $"+{set.FlatDefense()} Defense";
                model.TotalPercentageDefense = $"+{set.PercentageDefense()}% Defense";
                model.TotalTenactiy = $"+{set.Tenacity()}% Tenacity";
                model.TotalCritAvoid = $"+{set.CritAvoid()}% Critical Avoidance";

                result.Add(model);
            }

            return result;
        }

        public static List<ModVM> GetModVMs(List<PlayerMod> mods, bool showCheckbox = false)
        {
            var result = new List<ModVM>();

            foreach (var mod in mods)
                result.Add(GetModVM(mod, showCheckbox));

            return result;
        }

        public static List<ModVM> GetModVMs(List<PlayerModsWithStat> statMods, bool showCheckbox = false)
        {
            var result = new List<ModVM>();

            var db = new ZelusDbContext();
            var mods = statMods.Select(sm => db.PlayerMods.Single(pm => pm.Id == sm.Id)).ToList();

            foreach (var mod in mods)
                result.Add(GetModVM(mod, showCheckbox));

            return result;
        }

        private static ModVM GetModVM(PlayerMod mod, bool showCheckbox)
        {
            var model = new ModVM();

            model.ShowCheckbox = showCheckbox;
            model.Id = mod.Id;
            model.Pips = mod.Pips;
            model.Set = (ModSets)mod.SetId;
            model.Slot = (ModSlots)mod.SlotId;

            model.CharacterName = mod.PlayerCharacter?.Unit?.Name ?? "Unassigned";
            model.CharacterUrl = GetCharacterUrl(mod);
            model.CharacterImg = mod.PlayerCharacter?.Unit?.Image ?? "";

            if (mod.PrimaryType != null)
                model.Primary = $"+{GetModStatValue(mod.PrimaryValue, (ModStatUnits)mod.PrimaryUnitsId)} {GetPrimaryModStatTypeName(mod.PrimaryType)}";

            if (mod.Secondary1Type != null)
                model.Secondary1 = $"+{GetModStatValue(mod.Secondary1Value, (ModStatUnits)mod.Secondary1UnitsId)} {GetModStatTypeName(mod.Secondary1Type)}";

            if (mod.Secondary2Type != null)
                model.Secondary2 = $"+{GetModStatValue(mod.Secondary2Value, (ModStatUnits)mod.Secondary2UnitsId)} {GetModStatTypeName(mod.Secondary2Type)}";

            if (mod.Secondary3Type != null)
                model.Secondary3 = $"+{GetModStatValue(mod.Secondary3Value, (ModStatUnits)mod.Secondary3UnitsId)} {GetModStatTypeName(mod.Secondary3Type)}";

            if (mod.Secondary4Type != null)
                model.Secondary4 = $"+{GetModStatValue(mod.Secondary4Value, (ModStatUnits)mod.Secondary4UnitsId)} {GetModStatTypeName(mod.Secondary4Type)}";

            return model;
        }

        private static string GetCharacterUrl(PlayerMod mod)
        {
            if (!mod.PlayerCharacterId.HasValue)
                return $"#";

            var unitUrl = mod.PlayerCharacter.Unit.Url;
            var urlParts = unitUrl.Split('/');
            var unitUrlName = Enumerable.Reverse(urlParts).Skip(1).Take(1);

            return $"{mod.Player.SwgohGgUrl}collection/{unitUrlName}/";
        }

        private static string GetModStatValue(decimal value, ModStatUnits units)
        {
            if (units == ModStatUnits.Flat)
                return $"{decimal.ToInt32(value)}";

            var percentage = Math.Round(value, 1);
            return $"{percentage}%";
        }

        private static string GetModStatTypeName(ModStatType type)
        {
            if ((ModStatTypes)type.Id == ModStatTypes.Critical_Chance)
                return $"Critical Chance";

            if ((ModStatTypes)type.Id == ModStatTypes.Critical_Damage)
                return $"Critical Damage";

            if ((ModStatTypes)type.Id == ModStatTypes.Critical_Avoidence)
                return $"Critical Avoidance";

            return type.Name;
        }

        private static string GetPrimaryModStatTypeName(ModStatType type)
        {
            if ((ModStatTypes)type.Id == ModStatTypes.Critical_Avoidence)
                return $"CrAvoid";

            if ((ModStatTypes)type.Id == ModStatTypes.Critical_Chance)
                return $"CC";

            if ((ModStatTypes)type.Id == ModStatTypes.Critical_Damage)
                return $"CD";

            if ((ModStatTypes)type.Id == ModStatTypes.Protection)
                return $"Prot";

            return type.Name;
        }
    }
}