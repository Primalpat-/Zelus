using Zelus.Data;

namespace Zelus.Web.Helpers.Extensions
{
    public static class PlayerModSetExtensions
    {
        #region Offensive Stats

        public static int Speed(this PlayerModSet set)
        {
            var result = default(int);

            result += set.Mod1.Speed();
            result += set.Mod2.Speed();
            result += set.Mod3.Speed();
            result += set.Mod4.Speed();
            result += set.Mod5.Speed();
            result += set.Mod6.Speed();

            return result;
        }

        public static decimal CritChance(this PlayerModSet set)
        {
            var result = default(decimal);

            result += set.Mod1.CritChance();
            result += set.Mod2.CritChance();
            result += set.Mod3.CritChance();
            result += set.Mod4.CritChance();
            result += set.Mod5.CritChance();
            result += set.Mod6.CritChance();

            return result;
        }

        public static decimal CritDamage(this PlayerModSet set)
        {
            var result = default(decimal);

            result += set.Mod1.CritDamage();
            result += set.Mod2.CritDamage();
            result += set.Mod3.CritDamage();
            result += set.Mod4.CritDamage();
            result += set.Mod5.CritDamage();
            result += set.Mod6.CritDamage();

            return result;
        }

        public static int FlatOffense(this PlayerModSet set)
        {
            var result = default(int);

            result += set.Mod1.FlatOffense();
            result += set.Mod2.FlatOffense();
            result += set.Mod3.FlatOffense();
            result += set.Mod4.FlatOffense();
            result += set.Mod5.FlatOffense();
            result += set.Mod6.FlatOffense();

            return result;
        }

        public static decimal PercentageOffense(this PlayerModSet set)
        {
            var result = default(decimal);

            result += set.Mod1.PercentageOffense();
            result += set.Mod2.PercentageOffense();
            result += set.Mod3.PercentageOffense();
            result += set.Mod4.PercentageOffense();
            result += set.Mod5.PercentageOffense();
            result += set.Mod6.PercentageOffense();

            return result;
        }

        public static decimal Potency(this PlayerModSet set)
        {
            var result = default(decimal);

            result += set.Mod1.Potency();
            result += set.Mod2.Potency();
            result += set.Mod3.Potency();
            result += set.Mod4.Potency();
            result += set.Mod5.Potency();
            result += set.Mod6.Potency();

            return result;
        }

        public static decimal Accuracy(this PlayerModSet set)
        {
            var result = default(decimal);

            result += set.Mod1.Accuracy();
            result += set.Mod2.Accuracy();
            result += set.Mod3.Accuracy();
            result += set.Mod4.Accuracy();
            result += set.Mod5.Accuracy();
            result += set.Mod6.Accuracy();

            return result;
        }

        #endregion

        #region Defense Stats

        public static int FlatProtection(this PlayerModSet set)
        {
            var result = default(int);

            result += set.Mod1.FlatProtection();
            result += set.Mod2.FlatProtection();
            result += set.Mod3.FlatProtection();
            result += set.Mod4.FlatProtection();
            result += set.Mod5.FlatProtection();
            result += set.Mod6.FlatProtection();

            return result;
        }

        public static decimal PercentageProtection(this PlayerModSet set)
        {
            var result = default(decimal);

            result += set.Mod1.PercentageProtection();
            result += set.Mod2.PercentageProtection();
            result += set.Mod3.PercentageProtection();
            result += set.Mod4.PercentageProtection();
            result += set.Mod5.PercentageProtection();
            result += set.Mod6.PercentageProtection();

            return result;
        }

        public static int FlatHealth(this PlayerModSet set)
        {
            var result = default(int);

            result += set.Mod1.FlatHealth();
            result += set.Mod2.FlatHealth();
            result += set.Mod3.FlatHealth();
            result += set.Mod4.FlatHealth();
            result += set.Mod5.FlatHealth();
            result += set.Mod6.FlatHealth();

            return result;
        }

        public static decimal PercentageHealth(this PlayerModSet set)
        {
            var result = default(decimal);

            result += set.Mod1.PercentageHealth();
            result += set.Mod2.PercentageHealth();
            result += set.Mod3.PercentageHealth();
            result += set.Mod4.PercentageHealth();
            result += set.Mod5.PercentageHealth();
            result += set.Mod6.PercentageHealth();

            return result;
        }

        public static int FlatDefense(this PlayerModSet set)
        {
            var result = default(int);

            result += set.Mod1.FlatDefense();
            result += set.Mod2.FlatDefense();
            result += set.Mod3.FlatDefense();
            result += set.Mod4.FlatDefense();
            result += set.Mod5.FlatDefense();
            result += set.Mod6.FlatDefense();

            return result;
        }

        public static decimal PercentageDefense(this PlayerModSet set)
        {
            var result = default(decimal);

            result += set.Mod1.PercentageDefense();
            result += set.Mod2.PercentageDefense();
            result += set.Mod3.PercentageDefense();
            result += set.Mod4.PercentageDefense();
            result += set.Mod5.PercentageDefense();
            result += set.Mod6.PercentageDefense();

            return result;
        }

        public static decimal Tenacity(this PlayerModSet set)
        {
            var result = default(decimal);

            result += set.Mod1.Tenacity();
            result += set.Mod2.Tenacity();
            result += set.Mod3.Tenacity();
            result += set.Mod4.Tenacity();
            result += set.Mod5.Tenacity();
            result += set.Mod6.Tenacity();

            return result;
        }

        public static decimal CritAvoid(this PlayerModSet set)
        {
            var result = default(decimal);

            result += set.Mod1.CritAvoid();
            result += set.Mod2.CritAvoid();
            result += set.Mod3.CritAvoid();
            result += set.Mod4.CritAvoid();
            result += set.Mod5.CritAvoid();
            result += set.Mod6.CritAvoid();

            return result;
        }

        #endregion
    }
}