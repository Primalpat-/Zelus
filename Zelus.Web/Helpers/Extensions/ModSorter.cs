using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zelus.Data;

namespace Zelus.Web.Helpers.Extensions
{
    public static class ModSorter
    {
        public static IQueryable<PlayerModsWithStat> ApplySorts(this IQueryable<PlayerModsWithStat> mods, List<int> sorts)
        {
            if (sorts == null || sorts.Count == 0)
                return mods.OrderByDescending(m => m.Speed);

            if (sorts.Count == 1)
                return mods.GetFirstSort(sorts);

            var orderedMods = mods.GetFirstSort(sorts);
            sorts.RemoveAt(0);

            foreach (var sort in sorts)
                orderedMods = orderedMods.GetNextSort(sort);

            return orderedMods;
        }

        private static IOrderedQueryable<PlayerModsWithStat> GetFirstSort(this IQueryable<PlayerModsWithStat> mods, List<int> sorts)
        {
            var firstSort = sorts[0];

            switch (firstSort)
            {
                case (int)ModStatTypes.Speed:
                    mods = mods.OrderByDescending(m => m.Speed);
                    break;
                case (int)ModStatTypes.Critical_Chance:
                    mods = mods.OrderByDescending(m => m.CritChance);
                    break;
                case (int)ModStatTypes.Critical_Damage:
                    mods = mods.OrderByDescending(m => m.CritDamage);
                    break;
                case (int)ModStatTypes.Offense + 100:
                    mods = mods.OrderByDescending(m => m.FlatOffense);
                    break;
                case (int)ModStatTypes.Offense + 200:
                    mods = mods.OrderByDescending(m => m.PercentageOffense);
                    break;
                case (int)ModStatTypes.Potency:
                    mods = mods.OrderByDescending(m => m.Potency);
                    break;
                case (int)ModStatTypes.Accuracy:
                    mods = mods.OrderByDescending(m => m.Accuracy);
                    break;
                case (int)ModStatTypes.Protection + 100:
                    mods = mods.OrderByDescending(m => m.FlatProtection);
                    break;
                case (int)ModStatTypes.Protection + 200:
                    mods = mods.OrderByDescending(m => m.PercentageProtection);
                    break;
                case (int)ModStatTypes.Health + 100:
                    mods = mods.OrderByDescending(m => m.FlatHealth);
                    break;
                case (int)ModStatTypes.Health + 200:
                    mods = mods.OrderByDescending(m => m.PercentageHealth);
                    break;
                case (int)ModStatTypes.Defense + 100:
                    mods = mods.OrderByDescending(m => m.FlatDefense);
                    break;
                case (int)ModStatTypes.Defense + 200:
                    mods = mods.OrderByDescending(m => m.PercentageDefense);
                    break;
                case (int)ModStatTypes.Tenacity:
                    mods = mods.OrderByDescending(m => m.Tenacity);
                    break;
                case (int)ModStatTypes.Critical_Avoidence:
                    mods = mods.OrderByDescending(m => m.CritAvoidance);
                    break;
                default:
                    break;
            }

            return (IOrderedQueryable<PlayerModsWithStat>) mods;
        }

        private static IOrderedQueryable<PlayerModsWithStat> GetNextSort(this IOrderedQueryable<PlayerModsWithStat> mods, int sort)
        {
            switch (sort)
            {
                case (int)ModStatTypes.Speed:
                    mods = mods.ThenByDescending(m => m.Speed);
                    break;
                case (int)ModStatTypes.Critical_Chance:
                    mods = mods.ThenByDescending(m => m.CritChance);
                    break;
                case (int)ModStatTypes.Critical_Damage:
                    mods = mods.ThenByDescending(m => m.CritDamage);
                    break;
                case (int)ModStatTypes.Offense + 100:
                    mods = mods.ThenByDescending(m => m.FlatOffense);
                    break;
                case (int)ModStatTypes.Offense + 200:
                    mods = mods.ThenByDescending(m => m.PercentageOffense);
                    break;
                case (int)ModStatTypes.Potency:
                    mods = mods.ThenByDescending(m => m.Potency);
                    break;
                case (int)ModStatTypes.Accuracy:
                    mods = mods.ThenByDescending(m => m.Accuracy);
                    break;
                case (int)ModStatTypes.Protection + 100:
                    mods = mods.ThenByDescending(m => m.FlatProtection);
                    break;
                case (int)ModStatTypes.Protection + 200:
                    mods = mods.ThenByDescending(m => m.PercentageProtection);
                    break;
                case (int)ModStatTypes.Health + 100:
                    mods = mods.ThenByDescending(m => m.FlatHealth);
                    break;
                case (int)ModStatTypes.Health + 200:
                    mods = mods.ThenByDescending(m => m.PercentageHealth);
                    break;
                case (int)ModStatTypes.Defense + 100:
                    mods = mods.ThenByDescending(m => m.FlatDefense);
                    break;
                case (int)ModStatTypes.Defense + 200:
                    mods = mods.ThenByDescending(m => m.PercentageDefense);
                    break;
                case (int)ModStatTypes.Tenacity:
                    mods = mods.ThenByDescending(m => m.Tenacity);
                    break;
                case (int)ModStatTypes.Critical_Avoidence:
                    mods = mods.ThenByDescending(m => m.CritAvoidance);
                    break;
                default:
                    break;
            }

            return mods;
        }
    }
}