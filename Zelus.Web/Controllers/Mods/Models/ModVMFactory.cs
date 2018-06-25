using System;
using System.Collections.Generic;
using System.Linq;
using Zelus.Data;
using Zelus.Logic.Extensions.Entities.PlayerMods;
using Zelus.Logic.Extensions.Entities.PlayerModSets;

namespace Zelus.Web.Controllers.Mods.Models
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

        public static List<CharacterVM> GetCharacterVMs(ZelusDbContext db)
        {
            var result = db.Units
                           .OrderBy(u => u.Name)
                           .Select(u => new CharacterVM { Id = u.Id, Name = u.Name })
                           .ToList();

            result.Insert(0, new CharacterVM { Id = 0, Name = "Select a character..." });

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

            model.IsInModSet = mod.IsInModSet();
            model.ShowCheckbox = showCheckbox;
            model.Id = mod.Id;
            model.Pips = mod.Pips;

            model.Set = (ModSets)mod.SetId;
            model.Slot = (ModSlots)mod.SlotId;
            model.ModImg = GetModImg(mod);

            model.CharacterId = mod.PlayerCharacter?.Unit?.Id ?? 0;
            model.CharacterName = mod.PlayerCharacter?.Unit?.Name ?? "Unassigned";
            model.CharacterUrl = GetCharacterUrl(mod);
            model.CharacterImg = mod.PlayerCharacter?.Unit?.Image ?? "/Content/Images/unassigned.jpg";

            model.PrimaryType = mod.PrimaryTypeId;

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

            model.Speed = mod.Speed();
            model.CritChance = mod.CritChance();
            model.CritDamage = mod.CritDamage();
            model.FlatOffense = mod.FlatOffense();
            model.PercentageOffense = mod.PercentageOffense();
            model.Potency = mod.Potency();
            model.Accuracy = mod.Accuracy();

            model.FlatProtection = mod.FlatProtection();
            model.PercentageProtection = mod.PercentageProtection();
            model.FlatHealth = mod.FlatHealth();
            model.PercentageHealth = mod.PercentageHealth();
            model.FlatDefense = mod.FlatDefense();
            model.PercentageDefense = mod.PercentageDefense();
            model.Tenacity = mod.Tenacity();
            model.CritAvoidance = mod.CritAvoid();

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

        private static string GetModImg(PlayerMod mod)
        {
            var set = (ModSets) mod.SetId;
            var slot = (ModSlots) mod.SlotId;

            switch(set)
            {
                case ModSets.Health:
                    switch (slot)
                    {
                        case ModSlots.Square:
                            return "/Content/Images/Mods/health-square.png";
                        case ModSlots.Arrow:
                            return "/Content/Images/Mods/health-arrow.png";
                        case ModSlots.Diamond:
                            return "/Content/Images/Mods/health-diamond.png";
                        case ModSlots.Triangle:
                            return "/Content/Images/Mods/health-triangle.png";
                        case ModSlots.Circle:
                            return "/Content/Images/Mods/health-circle.png";
                        case ModSlots.Cross:
                            return "/Content/Images/Mods/health-cross.png";
                        default:
                            return "/Content/Images/Mods/health-square.png";
                    }
                case ModSets.Offense:
                    switch (slot)
                    {
                        case ModSlots.Square:
                            return "/Content/Images/Mods/offense-square.png";
                        case ModSlots.Arrow:
                            return "/Content/Images/Mods/offense-arrow.png";
                        case ModSlots.Diamond:
                            return "/Content/Images/Mods/offense-diamond.png";
                        case ModSlots.Triangle:
                            return "/Content/Images/Mods/offense-triangle.png";
                        case ModSlots.Circle:
                            return "/Content/Images/Mods/offense-circle.png";
                        case ModSlots.Cross:
                            return "/Content/Images/Mods/offense-cross.png";
                        default:
                            return "/Content/Images/Mods/offense-square.png";
                    }
                case ModSets.Defense:
                    switch (slot)
                    {
                        case ModSlots.Square:
                            return "/Content/Images/Mods/defense-square.png";
                        case ModSlots.Arrow:
                            return "/Content/Images/Mods/defense-arrow.png";
                        case ModSlots.Diamond:
                            return "/Content/Images/Mods/defense-diamond.png";
                        case ModSlots.Triangle:
                            return "/Content/Images/Mods/defense-triangle.png";
                        case ModSlots.Circle:
                            return "/Content/Images/Mods/defense-circle.png";
                        case ModSlots.Cross:
                            return "/Content/Images/Mods/defense-cross.png";
                        default:
                            return "/Content/Images/Mods/defense-square.png";
                    }
                case ModSets.Speed:
                    switch (slot)
                    {
                        case ModSlots.Square:
                            return "/Content/Images/Mods/speed-square.png";
                        case ModSlots.Arrow:
                            return "/Content/Images/Mods/speed-arrow.png";
                        case ModSlots.Diamond:
                            return "/Content/Images/Mods/speed-diamond.png";
                        case ModSlots.Triangle:
                            return "/Content/Images/Mods/speed-triangle.png";
                        case ModSlots.Circle:
                            return "/Content/Images/Mods/speed-circle.png";
                        case ModSlots.Cross:
                            return "/Content/Images/Mods/speed-cross.png";
                        default:
                            return "/Content/Images/Mods/speed-square.png";
                    }
                case ModSets.Crit_Chance:
                    switch (slot)
                    {
                        case ModSlots.Square:
                            return "/Content/Images/Mods/cc-square.png";
                        case ModSlots.Arrow:
                            return "/Content/Images/Mods/cc-arrow.png";
                        case ModSlots.Diamond:
                            return "/Content/Images/Mods/cc-diamond.png";
                        case ModSlots.Triangle:
                            return "/Content/Images/Mods/cc-triangle.png";
                        case ModSlots.Circle:
                            return "/Content/Images/Mods/cc-circle.png";
                        case ModSlots.Cross:
                            return "/Content/Images/Mods/cc-cross.png";
                        default:
                            return "/Content/Images/Mods/cc-square.png";
                    }
                case ModSets.Crit_Damage:
                    switch (slot)
                    {
                        case ModSlots.Square:
                            return "/Content/Images/Mods/cd-square.png";
                        case ModSlots.Arrow:
                            return "/Content/Images/Mods/cd-arrow.png";
                        case ModSlots.Diamond:
                            return "/Content/Images/Mods/cd-diamond.png";
                        case ModSlots.Triangle:
                            return "/Content/Images/Mods/cd-triangle.png";
                        case ModSlots.Circle:
                            return "/Content/Images/Mods/cd-circle.png";
                        case ModSlots.Cross:
                            return "/Content/Images/Mods/cd-cross.png";
                        default:
                            return "/Content/Images/Mods/cd-square.png";
                    }
                case ModSets.Potency:
                    switch (slot)
                    {
                        case ModSlots.Square:
                            return "/Content/Images/Mods/potency-square.png";
                        case ModSlots.Arrow:
                            return "/Content/Images/Mods/potency-arrow.png";
                        case ModSlots.Diamond:
                            return "/Content/Images/Mods/potency-diamond.png";
                        case ModSlots.Triangle:
                            return "/Content/Images/Mods/potency-triangle.png";
                        case ModSlots.Circle:
                            return "/Content/Images/Mods/potency-circle.png";
                        case ModSlots.Cross:
                            return "/Content/Images/Mods/potency-cross.png";
                        default:
                            return "/Content/Images/Mods/potency-square.png";
                    }
                case ModSets.Tenacity:
                    switch (slot)
                    {
                        case ModSlots.Square:
                            return "/Content/Images/Mods/tenacity-square.png";
                        case ModSlots.Arrow:
                            return "/Content/Images/Mods/tenacity-arrow.png";
                        case ModSlots.Diamond:
                            return "/Content/Images/Mods/tenacity-diamond.png";
                        case ModSlots.Triangle:
                            return "/Content/Images/Mods/tenacity-triangle.png";
                        case ModSlots.Circle:
                            return "/Content/Images/Mods/tenacity-circle.png";
                        case ModSlots.Cross:
                            return "/Content/Images/Mods/tenacity-cross.png";
                        default:
                            return "/Content/Images/Mods/tenacity-square.png";
                    }
                default:
                    return "/Content/Images/Mods/health-square.png";
            }
        }
    }
}