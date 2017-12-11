using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using Z.Core.Extensions;

namespace Zelus.Data
{
    public partial class PlayerModsWithStat
    {
        public static Expression<Func<PlayerModsWithStat, bool>> IsNotInPlayerSet()
        {
            return mod => mod.IsInPlayerSet == 0;
        }

        public static Expression<Func<PlayerModsWithStat, bool>> BelongsToPlayer(int playerId)
        {
            return mod => mod.PlayerId == playerId;
        }

        public static Expression<Func<PlayerModsWithStat, bool>> IsOfSet(List<string> sets)
        {
            var predicate = PredicateBuilder.New<PlayerModsWithStat>(true);

            if (sets.IsNull() || sets.Count == 0)
                return predicate;

            foreach (var setName in sets)
            {
                var setEnum = (ModSets) Enum.Parse(typeof(ModSets), setName);
                predicate = predicate.Or(m => m.SetId == (int) setEnum);
            }

            return predicate;
        }

        public static Expression<Func<PlayerModsWithStat, bool>> IsOfSlot(string slot)
        {
            var predicate = PredicateBuilder.New<PlayerModsWithStat>(true);

            var slotEnum = (ModSlots) Enum.Parse(typeof(ModSlots), slot);
            predicate = predicate.And(m => m.SlotId == (int) slotEnum);

            return predicate;
        }

        public static Expression<Func<PlayerModsWithStat, bool>> IsOfPrimary(int primaryTypeId)
        {
            var predicate = PredicateBuilder.New<PlayerModsWithStat>(true);

            if (primaryTypeId == 0)
                return predicate;

            predicate = predicate.And(m => m.PrimaryTypeId == primaryTypeId);

            return predicate;
        }
    }
}