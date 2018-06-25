using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelus.Data;

namespace Zelus.Logic.Extensions.Entities.PlayerModsWithStats
{
    public static class PlayerModsWithStatExtensions
    {
        public static string PrimaryText(this PlayerModsWithStat mod)
        {
            return GetPrimaryText(mod, (ModStatTypes)mod.PrimaryTypeId);
        }

        public static List<string> GetSecondaries(this PlayerModsWithStat mod)
        {
            var result = new List<string>();

            if (mod.Speed.HasValue && mod.Speed > 0.00m)
                result.Add($"{decimal.ToInt32((decimal)mod.Speed)} Speed");

            if (mod.CritChance.HasValue && mod.CritChance > 0.00m)
                result.Add($"{FormatPercentageValue(mod.CritChance)} Critical Chance");

            if (mod.FlatOffense.HasValue && mod.FlatOffense > 0.00m)
                result.Add($"{decimal.ToInt32((decimal)mod.FlatOffense)} Offense");

            if (mod.PercentageOffense.HasValue && mod.PercentageOffense > 0.00m)
                result.Add($"{FormatPercentageValue(mod.PercentageOffense)} Offense");

            if (mod.Potency.HasValue && mod.Potency > 0.00m)
                result.Add($"{FormatPercentageValue(mod.Potency)} Potency");

            if (mod.FlatProtection.HasValue && mod.FlatProtection > 0.00m)
                result.Add($"{decimal.ToInt32((decimal)mod.FlatProtection)} Protection");

            if (mod.PercentageProtection.HasValue && mod.PercentageProtection > 0.00m)
                result.Add($"{FormatPercentageValue(mod.PercentageProtection)} Protection");

            if (mod.FlatHealth.HasValue && mod.FlatHealth > 0.00m)
                result.Add($"{decimal.ToInt32((decimal)mod.FlatHealth)} Health");

            if (mod.PercentageHealth.HasValue && mod.PercentageHealth > 0.00m)
                result.Add($"{FormatPercentageValue(mod.PercentageHealth)} Health");

            if (mod.FlatDefense.HasValue && mod.FlatDefense > 0.00m)
                result.Add($"{decimal.ToInt32((decimal)mod.FlatDefense)} Defense");

            if (mod.PercentageDefense.HasValue && mod.PercentageDefense > 0.00m)
                result.Add($"{FormatPercentageValue(mod.PercentageDefense)} Defense");

            if (mod.Tenacity.HasValue && mod.Tenacity > 0.00m)
                result.Add($"{FormatPercentageValue(mod.Tenacity)} Tenacity");

            return result;
        }

        private static string GetPrimaryText(PlayerModsWithStat mod, ModStatTypes type)
        {
            switch (type)
            {
                case ModStatTypes.Offense:
                    return $"{FormatPercentageValue(mod.PercentageOffense)} Offense";
                case ModStatTypes.Defense:
                    return $"{FormatPercentageValue(mod.PercentageDefense)} Defense";
                case ModStatTypes.Health:
                    return $"{FormatPercentageValue(mod.PercentageHealth)} Health";
                case ModStatTypes.Protection:
                    return $"{FormatPercentageValue(mod.PercentageProtection)} Prot";
                case ModStatTypes.Speed:
                    return $"{decimal.ToInt32((decimal)mod.Speed)} Speed";
                case ModStatTypes.Critical_Avoidence:
                    return $"{FormatPercentageValue(mod.CritAvoidance)} Avoidance";
                case ModStatTypes.Accuracy:
                    return $"{FormatPercentageValue(mod.Accuracy)} Accuracy";
                case ModStatTypes.Critical_Damage:
                    return $"{FormatPercentageValue(mod.CritDamage)} CD";
                case ModStatTypes.Critical_Chance:
                    return $"{FormatPercentageValue(mod.CritChance)} CC";
                case ModStatTypes.Potency:
                    return $"{FormatPercentageValue(mod.Potency)} Potency";
                case ModStatTypes.Tenacity:
                    return $"{FormatPercentageValue(mod.Tenacity)} Tenacity";
                default:
                    return $"Unknown";
            }
        }

        private static string FormatPercentageValue(decimal? value)
        {
            return $"{Math.Round((decimal)value, 1).ToString("0.0").Replace(".0", string.Empty)}%";
        }
    }
}
