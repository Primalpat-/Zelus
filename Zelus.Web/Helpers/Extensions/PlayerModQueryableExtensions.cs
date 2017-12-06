using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Z.Core.Extensions;
using Zelus.Data;

namespace Zelus.Web.Helpers.Extensions
{
    public static class PlayerModQueryableExtensions
    {
        public static IQueryable<PlayerMod> FilterByPlayer(this IQueryable<PlayerMod> mods, int playerId)
        {
            var predicate = PredicateBuilder.New<PlayerMod>(true);

            predicate = predicate.And(m => m.PlayerId == playerId);

            return mods.AsExpandable().Where(predicate);
        }

        public static IQueryable<PlayerMod> FilterBySets(this IQueryable<PlayerMod> mods, List<string> sets)
        {
            if (sets.IsNull() || sets.Count == 0)
                return mods;

            var predicate = PredicateBuilder.New<PlayerMod>(true);

            foreach (var setName in sets)
            {
                var setEnum = (ModSets)Enum.Parse(typeof(ModSets), setName);
                predicate = predicate.Or(m => m.SetId == (int)setEnum);
            }

            return mods.AsExpandable().Where(predicate);
        }

        public static IQueryable<PlayerMod> FilterBySlot(this IQueryable<PlayerMod> mods, string slot)
        {
            var predicate = PredicateBuilder.New<PlayerMod>(true);

            var slotEnum = (ModSlots) Enum.Parse(typeof(ModSlots), slot);
            predicate = predicate.And(m => m.SlotId == (int)slotEnum);

            return mods.AsExpandable().Where(predicate);
        }

        public static IQueryable<PlayerMod> FilterByPrimary(this IQueryable<PlayerMod> mods, int primary)
        {
            if (primary == 0)
                return mods;

            var predicate = PredicateBuilder.New<PlayerMod>(true);
            
            predicate = predicate.And(m => m.PrimaryTypeId == primary);

            return mods.AsExpandable().Where(predicate);
        }
    }
}