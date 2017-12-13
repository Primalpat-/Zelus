using System.Linq;
using Z.Core.Extensions;
using Zelus.Data;

namespace Zelus.Logic.Extensions.Entities.PlayerMods
{
    public static class PlayerModExtensions
    {
        public static bool IsInModSet(this PlayerMod mod)
        {
            return mod.Mod1.Any() || 
                   mod.Mod2.Any() || 
                   mod.Mod3.Any() ||
                   mod.Mod4.Any() || 
                   mod.Mod5.Any() || 
                   mod.Mod6.Any();
        }

        #region Offensive Stats

        public static int Speed(this PlayerMod mod)
        {
            var speed = default(int);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Speed)
                speed += mod.PrimaryValue.ToInt32();

            if (mod.Secondary1TypeId == (int)ModStatTypes.Speed)
                speed += mod.Secondary1Value.ToInt32();
            
            if (mod.Secondary2TypeId == (int)ModStatTypes.Speed)
                speed += mod.Secondary2Value.ToInt32();

            if (mod.Secondary3TypeId == (int)ModStatTypes.Speed)
                speed += mod.Secondary3Value.ToInt32();

            if (mod.Secondary4TypeId == (int)ModStatTypes.Speed)
                speed += mod.Secondary4Value.ToInt32();

            return speed;
        }

        public static decimal CritChance(this PlayerMod mod)
        {
            var critChance = default(decimal);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Critical_Chance)
                critChance += mod.PrimaryValue.ToInt32();

            if (mod.Secondary1TypeId == (int)ModStatTypes.Critical_Chance)
                critChance += mod.Secondary1Value;

            if (mod.Secondary2TypeId == (int)ModStatTypes.Critical_Chance)
                critChance += mod.Secondary2Value;

            if (mod.Secondary3TypeId == (int)ModStatTypes.Critical_Chance)
                critChance += mod.Secondary3Value;

            if (mod.Secondary4TypeId == (int)ModStatTypes.Critical_Chance)
                critChance += mod.Secondary4Value;

            return critChance;
        }

        public static decimal CritDamage(this PlayerMod mod)
        {
            var critDamage = default(decimal);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Critical_Damage)
                critDamage += mod.PrimaryValue;

            if (mod.Secondary1TypeId == (int)ModStatTypes.Critical_Damage)
                critDamage += mod.Secondary1Value;

            if (mod.Secondary2TypeId == (int)ModStatTypes.Critical_Damage)
                critDamage += mod.Secondary2Value;

            if (mod.Secondary3TypeId == (int)ModStatTypes.Critical_Damage)
                critDamage += mod.Secondary3Value;

            if (mod.Secondary4TypeId == (int)ModStatTypes.Critical_Damage)
                critDamage += mod.Secondary4Value;

            return critDamage;
        }

        public static int FlatOffense(this PlayerMod mod)
        {
            var offense = default(int);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Offense &&
                mod.PrimaryUnitsId == (int)ModStatUnits.Flat)
                offense += mod.PrimaryValue.ToInt32();

            if (mod.Secondary1TypeId == (int)ModStatTypes.Offense &&
                mod.Secondary1UnitsId == (int)ModStatUnits.Flat)
                offense += mod.Secondary1Value.ToInt32();

            if (mod.Secondary2TypeId == (int)ModStatTypes.Offense &&
                mod.Secondary2UnitsId == (int)ModStatUnits.Flat)
                offense += mod.Secondary2Value.ToInt32();

            if (mod.Secondary3TypeId == (int)ModStatTypes.Offense &&
                mod.Secondary3UnitsId == (int)ModStatUnits.Flat)
                offense += mod.Secondary3Value.ToInt32();

            if (mod.Secondary4TypeId == (int)ModStatTypes.Offense &&
                mod.Secondary4UnitsId == (int)ModStatUnits.Flat)
                offense += mod.Secondary4Value.ToInt32();

            return offense;
        }

        public static decimal PercentageOffense(this PlayerMod mod)
        {
            var offense = default(decimal);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Offense &&
                mod.PrimaryUnitsId == (int)ModStatUnits.Percentage)
                offense += mod.PrimaryValue;

            if (mod.Secondary1TypeId == (int)ModStatTypes.Offense &&
                mod.Secondary1UnitsId == (int)ModStatUnits.Percentage)
                offense += mod.Secondary1Value;

            if (mod.Secondary2TypeId == (int)ModStatTypes.Offense &&
                mod.Secondary2UnitsId == (int)ModStatUnits.Percentage)
                offense += mod.Secondary2Value;

            if (mod.Secondary3TypeId == (int)ModStatTypes.Offense &&
                mod.Secondary3UnitsId == (int)ModStatUnits.Percentage)
                offense += mod.Secondary3Value;

            if (mod.Secondary4TypeId == (int)ModStatTypes.Offense &&
                mod.Secondary4UnitsId == (int)ModStatUnits.Percentage)
                offense += mod.Secondary4Value;

            return offense;
        }

        public static decimal Potency(this PlayerMod mod)
        {
            var potency = default(decimal);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Potency)
                potency += mod.PrimaryValue;

            if (mod.Secondary1TypeId == (int)ModStatTypes.Potency)
                potency += mod.Secondary1Value;

            if (mod.Secondary2TypeId == (int)ModStatTypes.Potency)
                potency += mod.Secondary2Value;

            if (mod.Secondary3TypeId == (int)ModStatTypes.Potency)
                potency += mod.Secondary3Value;

            if (mod.Secondary4TypeId == (int)ModStatTypes.Potency)
                potency += mod.Secondary4Value;

            return potency;
        }

        public static decimal Accuracy(this PlayerMod mod)
        {
            var accuracy = default(decimal);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Accuracy)
                accuracy += mod.PrimaryValue;

            if (mod.Secondary1TypeId == (int)ModStatTypes.Accuracy)
                accuracy += mod.Secondary1Value;

            if (mod.Secondary2TypeId == (int)ModStatTypes.Accuracy)
                accuracy += mod.Secondary2Value;

            if (mod.Secondary3TypeId == (int)ModStatTypes.Accuracy)
                accuracy += mod.Secondary3Value;

            if (mod.Secondary4TypeId == (int)ModStatTypes.Accuracy)
                accuracy += mod.Secondary4Value;

            return accuracy;
        }

        #endregion

        #region Defensive Stats

        public static int FlatProtection(this PlayerMod mod)
        {
            var protection = default(int);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Protection &&
                mod.PrimaryUnitsId == (int)ModStatUnits.Flat)
                protection += mod.PrimaryValue.ToInt32();

            if (mod.Secondary1TypeId == (int)ModStatTypes.Protection &&
                mod.Secondary1UnitsId == (int)ModStatUnits.Flat)
                protection += mod.Secondary1Value.ToInt32();

            if (mod.Secondary2TypeId == (int)ModStatTypes.Protection &&
                mod.Secondary2UnitsId == (int)ModStatUnits.Flat)
                protection += mod.Secondary2Value.ToInt32();

            if (mod.Secondary3TypeId == (int)ModStatTypes.Protection &&
                mod.Secondary3UnitsId == (int)ModStatUnits.Flat)
                protection += mod.Secondary3Value.ToInt32();

            if (mod.Secondary4TypeId == (int)ModStatTypes.Protection &&
                mod.Secondary4UnitsId == (int)ModStatUnits.Flat)
                protection += mod.Secondary4Value.ToInt32();

            return protection;
        }

        public static decimal PercentageProtection(this PlayerMod mod)
        {
            var protection = default(decimal);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Protection &&
                mod.PrimaryUnitsId == (int)ModStatUnits.Percentage)
                protection += mod.PrimaryValue;

            if (mod.Secondary1TypeId == (int)ModStatTypes.Protection &&
                mod.Secondary1UnitsId == (int)ModStatUnits.Percentage)
                protection += mod.Secondary1Value;

            if (mod.Secondary2TypeId == (int)ModStatTypes.Protection &&
                mod.Secondary2UnitsId == (int)ModStatUnits.Percentage)
                protection += mod.Secondary2Value;

            if (mod.Secondary3TypeId == (int)ModStatTypes.Protection &&
                mod.Secondary3UnitsId == (int)ModStatUnits.Percentage)
                protection += mod.Secondary3Value;

            if (mod.Secondary4TypeId == (int)ModStatTypes.Protection &&
                mod.Secondary4UnitsId == (int)ModStatUnits.Percentage)
                protection += mod.Secondary4Value;

            return protection;
        }

        public static int FlatHealth(this PlayerMod mod)
        {
            var health = default(int);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Health &&
                mod.PrimaryUnitsId == (int)ModStatUnits.Flat)
                health += mod.PrimaryValue.ToInt32();

            if (mod.Secondary1TypeId == (int)ModStatTypes.Health &&
                mod.Secondary1UnitsId == (int)ModStatUnits.Flat)
                health += mod.Secondary1Value.ToInt32();

            if (mod.Secondary2TypeId == (int)ModStatTypes.Health &&
                mod.Secondary2UnitsId == (int)ModStatUnits.Flat)
                health += mod.Secondary2Value.ToInt32();

            if (mod.Secondary3TypeId == (int)ModStatTypes.Health &&
                mod.Secondary3UnitsId == (int)ModStatUnits.Flat)
                health += mod.Secondary3Value.ToInt32();

            if (mod.Secondary4TypeId == (int)ModStatTypes.Health &&
                mod.Secondary4UnitsId == (int)ModStatUnits.Flat)
                health += mod.Secondary4Value.ToInt32();

            return health;
        }

        public static decimal PercentageHealth(this PlayerMod mod)
        {
            var health = default(decimal);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Health &&
                mod.PrimaryUnitsId == (int)ModStatUnits.Percentage)
                health += mod.PrimaryValue;

            if (mod.Secondary1TypeId == (int)ModStatTypes.Health &&
                mod.Secondary1UnitsId == (int)ModStatUnits.Percentage)
                health += mod.Secondary1Value;

            if (mod.Secondary2TypeId == (int)ModStatTypes.Health &&
                mod.Secondary2UnitsId == (int)ModStatUnits.Percentage)
                health += mod.Secondary2Value;

            if (mod.Secondary3TypeId == (int)ModStatTypes.Health &&
                mod.Secondary3UnitsId == (int)ModStatUnits.Percentage)
                health += mod.Secondary3Value;

            if (mod.Secondary4TypeId == (int)ModStatTypes.Health &&
                mod.Secondary4UnitsId == (int)ModStatUnits.Percentage)
                health += mod.Secondary4Value;

            return health;
        }

        public static int FlatDefense(this PlayerMod mod)
        {
            var defense = default(int);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Defense &&
                mod.PrimaryUnitsId == (int)ModStatUnits.Flat)
                defense += mod.PrimaryValue.ToInt32();

            if (mod.Secondary1TypeId == (int)ModStatTypes.Defense &&
                mod.Secondary1UnitsId == (int)ModStatUnits.Flat)
                defense += mod.Secondary1Value.ToInt32();

            if (mod.Secondary2TypeId == (int)ModStatTypes.Defense &&
                mod.Secondary2UnitsId == (int)ModStatUnits.Flat)
                defense += mod.Secondary2Value.ToInt32();

            if (mod.Secondary3TypeId == (int)ModStatTypes.Defense &&
                mod.Secondary3UnitsId == (int)ModStatUnits.Flat)
                defense += mod.Secondary3Value.ToInt32();

            if (mod.Secondary4TypeId == (int)ModStatTypes.Defense &&
                mod.Secondary4UnitsId == (int)ModStatUnits.Flat)
                defense += mod.Secondary4Value.ToInt32();

            return defense;
        }

        public static decimal PercentageDefense(this PlayerMod mod)
        {
            var defense = default(decimal);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Defense &&
                mod.PrimaryUnitsId == (int)ModStatUnits.Percentage)
                defense += mod.PrimaryValue;

            if (mod.Secondary1TypeId == (int)ModStatTypes.Defense &&
                mod.Secondary1UnitsId == (int)ModStatUnits.Percentage)
                defense += mod.Secondary1Value;

            if (mod.Secondary2TypeId == (int)ModStatTypes.Defense &&
                mod.Secondary2UnitsId == (int)ModStatUnits.Percentage)
                defense += mod.Secondary2Value;

            if (mod.Secondary3TypeId == (int)ModStatTypes.Defense &&
                mod.Secondary3UnitsId == (int)ModStatUnits.Percentage)
                defense += mod.Secondary3Value;

            if (mod.Secondary4TypeId == (int)ModStatTypes.Defense &&
                mod.Secondary4UnitsId == (int)ModStatUnits.Percentage)
                defense += mod.Secondary4Value;

            return defense;
        }

        public static decimal Tenacity(this PlayerMod mod)
        {
            var tenacity = default(decimal);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Tenacity)
                tenacity += mod.PrimaryValue;

            if (mod.Secondary1TypeId == (int)ModStatTypes.Tenacity)
                tenacity += mod.Secondary1Value;

            if (mod.Secondary2TypeId == (int)ModStatTypes.Tenacity)
                tenacity += mod.Secondary2Value;

            if (mod.Secondary3TypeId == (int)ModStatTypes.Tenacity)
                tenacity += mod.Secondary3Value;

            if (mod.Secondary4TypeId == (int)ModStatTypes.Tenacity)
                tenacity += mod.Secondary4Value;

            return tenacity;
        }

        public static decimal CritAvoid(this PlayerMod mod)
        {
            var critAvoid = default(decimal);

            if (mod.PrimaryTypeId == (int)ModStatTypes.Critical_Avoidence)
                critAvoid += mod.PrimaryValue;

            if (mod.Secondary1TypeId == (int)ModStatTypes.Critical_Avoidence)
                critAvoid += mod.Secondary1Value;

            if (mod.Secondary2TypeId == (int)ModStatTypes.Critical_Avoidence)
                critAvoid += mod.Secondary2Value;

            if (mod.Secondary3TypeId == (int)ModStatTypes.Critical_Avoidence)
                critAvoid += mod.Secondary3Value;

            if (mod.Secondary4TypeId == (int)ModStatTypes.Critical_Avoidence)
                critAvoid += mod.Secondary4Value;

            return critAvoid;
        }

        #endregion
    }
}