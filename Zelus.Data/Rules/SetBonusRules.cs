namespace Zelus.Data.Rules
{
    public static class SetBonusRules
    {
        public static int NumberRequiredForBonus(this ModSets set)
        {
            if (set == ModSets.Crit_Damage ||
                set == ModSets.Offense ||
                set == ModSets.Speed)
                return 4;

            return 2;
        }
    }
}