using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Z.Core.Extensions;
using Zelus.Data;

namespace Zelus.Web.Helpers.Extensions
{
    public static class PlayerModExtensions
    {
        public static int SecondarySpeed(this PlayerMod mod)
        {
            var speed = default(int);

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

        public static decimal SecondaryTenacity(this PlayerMod mod)
        {
            var tenacity = default(decimal);

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

        public static decimal SecondaryPotency(this PlayerMod mod)
        {
            var potency = default(decimal);

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

        public static decimal SecondaryCritChance(this PlayerMod mod)
        {
            var critChance = default(decimal);

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

        public static int SecondaryFlatHealth(this PlayerMod mod)
        {
            var health = default(int);

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

        public static decimal SecondaryPercentageHealth(this PlayerMod mod)
        {
            var health = default(decimal);

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

        public static int SecondaryFlatProtection(this PlayerMod mod)
        {
            var protection = default(int);

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

        public static decimal SecondaryPercentageProtection(this PlayerMod mod)
        {
            var protection = default(decimal);

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

        public static int SecondaryFlatOffense(this PlayerMod mod)
        {
            var offense = default(int);

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

        public static decimal SecondaryPercentageOffense(this PlayerMod mod)
        {
            var offense = default(decimal);

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

        public static int SecondaryFlatDefense(this PlayerMod mod)
        {
            var defense = default(int);

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

        public static decimal SecondaryPercentageDefense(this PlayerMod mod)
        {
            var defense = default(decimal);

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
    }
}