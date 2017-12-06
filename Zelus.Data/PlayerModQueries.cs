using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Z.Core.Extensions;

namespace Zelus.Data
{
    public partial class PlayerMod
    {
        public static Expression<Func<PlayerMod, bool>> IsNotInPlayerSet()
        {
            return mod => mod.Mod1.FirstOrDefault() == null &&
                          mod.Mod2.FirstOrDefault() == null &&
                          mod.Mod3.FirstOrDefault() == null &&
                          mod.Mod4.FirstOrDefault() == null &&
                          mod.Mod5.FirstOrDefault() == null &&
                          mod.Mod6.FirstOrDefault() == null;
        }

        public static Expression<Func<PlayerMod, bool>> BelongsToPlayer(int playerId)
        {
            return mod => mod.PlayerId == playerId;
        }

        public static Expression<Func<PlayerMod, bool>> IsOfSet(List<string> sets)
        {
            var predicate = PredicateBuilder.New<Data.PlayerMod>(true);

            if (sets.IsNull() || sets.Count == 0)
                return predicate;

            foreach (var setName in sets)
            {
                var setEnum = (ModSets)Enum.Parse(typeof(ModSets), setName);
                predicate = predicate.Or(m => m.SetId == (int)setEnum);
            }

            return predicate;
        }

        public static Expression<Func<PlayerMod, bool>> IsOfSlot(string slot)
        {
            var predicate = PredicateBuilder.New<Data.PlayerMod>(true);

            var slotEnum = (ModSlots)Enum.Parse(typeof(ModSlots), slot);
            predicate = predicate.And(m => m.SlotId == (int)slotEnum);

            return predicate;
        }

        public static Expression<Func<PlayerMod, bool>> IsOfPrimary(int primaryTypeId)
        {
            var predicate = PredicateBuilder.New<Data.PlayerMod>(true);

            if (primaryTypeId == 0)
                return predicate;

            predicate = predicate.And(m => m.PrimaryTypeId == primaryTypeId);

            return predicate;
        }
    }
}