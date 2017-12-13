using System;

namespace Zelus.Logic.Extensions.VariableManipulation
{
    public static class RomanNumeralExtensions
    {
        public static int ToLevel(this string numeral)
        {
            switch (numeral)
            {
                case "I":
                    return 1;
                case "II":
                    return 2;
                case "III":
                    return 3;
                case "IV":
                    return 4;
                case "V":
                    return 5;
                case "VI":
                    return 6;
                case "VII":
                    return 7;
                case "VIII":
                    return 8;
                case "IX":
                    return 9;
                case "X":
                    return 10;
                case "XI":
                    return 11;
                case "XII":
                    return 12;
                default:
                    throw new Exception("Unhandled gear level.");
            }
        }

        public static string ToNumeral(this int level)
        {
            switch (level)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                case 4:
                    return "IV";
                case 5:
                    return "V";
                case 6:
                    return "VI";
                case 7:
                    return "VII";
                case 8:
                    return "VIII";
                case 9:
                    return "IX";
                case 10:
                    return "X";
                case 11:
                    return "XI";
                case 12:
                    return "XII";
                default:
                    throw new Exception("Unhandled gear level.");
            }
        }
    }
}