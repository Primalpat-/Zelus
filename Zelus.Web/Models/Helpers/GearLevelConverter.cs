using System;

namespace Zelus.Web.Models.Helpers
{
    public static class GearLevelConverter
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
                default:
                    throw new Exception("Unhandled gear level.");
            }
        }

        public static string ToStyle(this int level)
        {
            switch (level)
            {
                case 1:
                    return "char-portrait-full-gear-t1";
                case 2:
                    return "char-portrait-full-gear-t2";
                case 3:
                    return "char-portrait-full-gear-t3";
                case 4:
                    return "char-portrait-full-gear-t4";
                case 5:
                    return "char-portrait-full-gear-t5";
                case 6:
                    return "char-portrait-full-gear-t6";
                case 7:
                    return "char-portrait-full-gear-t7";
                case 8:
                    return "char-portrait-full-gear-t8";
                case 9:
                    return "char-portrait-full-gear-t9";
                case 10:
                    return "char-portrait-full-gear-t10";
                case 11:
                    return "char-portrait-full-gear-t11";
                default:
                    throw new Exception("Unhandled gear level.");
            }
        }
    }
}