using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using Z.Core.Extensions;
using Zelus.Data.Rules;

namespace Zelus.Data
{
    public partial class PlayerModSet
    {
        public static Expression<Func<PlayerModSet, bool>> BelongsToPlayer(int playerId)
        {
            return set => set.PlayerId == playerId;
        }

        public static Expression<Func<PlayerModSet, bool>> HasSetBonus(List<string> setNames)
        {
            var predicate = PredicateBuilder.New<PlayerModSet>(true);

            if (setNames.IsNull() || setNames.Count == 0)
                return predicate;

            foreach (var setName in setNames)
            {
                var setEnum = (ModSets) Enum.Parse(typeof(ModSets), setName);
                var requiredMods = setEnum.NumberRequiredForBonus();

                if (requiredMods == 4)
                    predicate = predicate.Or(s => (s.Mod3.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod2.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod2.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod2.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod2.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod2.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod2.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod2.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod2.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod2.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod2.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum));
                else
                    predicate = predicate.Or(s => (s.Mod1.SetId == (int)setEnum && s.Mod2.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum) ||
                                                  (s.Mod1.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod2.SetId == (int)setEnum && s.Mod3.SetId == (int)setEnum) ||
                                                  (s.Mod2.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum) ||
                                                  (s.Mod2.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum) ||
                                                  (s.Mod2.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod3.SetId == (int)setEnum && s.Mod4.SetId == (int)setEnum) ||
                                                  (s.Mod3.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum) ||
                                                  (s.Mod3.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod4.SetId == (int)setEnum && s.Mod5.SetId == (int)setEnum) ||
                                                  (s.Mod4.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum) ||
                                                  (s.Mod5.SetId == (int)setEnum && s.Mod6.SetId == (int)setEnum));
            }

            return predicate;
        }
    }
}
