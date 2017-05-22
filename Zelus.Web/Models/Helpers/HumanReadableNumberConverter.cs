using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Z.Core.Extensions;

namespace Zelus.Web.Models.Helpers
{
    public static class HumanReadableNumberConverter
    {
        public static string ToHumanReadable(this int integer)
        {
            const int divisor = 1000;
            var numberOfDivisions = default(int);

            while (integer % divisor == 0)
            {
                numberOfDivisions++;
                integer = integer / divisor;
            }

            var prefix = default(decimal);
            if (integer > (divisor - 1))
            {
                numberOfDivisions++;
                prefix = integer.ToDecimal() / divisor.ToDecimal();
            }
            else
                prefix = integer.ToDecimal();

            return $"{prefix} {ThousandsToText(numberOfDivisions)}";
        }

        private static string ThousandsToText(int numberOfThousands)
        {
            switch (numberOfThousands)
            {
                case 1:
                    return "thousand";
                case 2:
                    return "million";
                case 3:
                    return "billion";
                case 4:
                    return "trillion";
                default:
                    throw new NotImplementedException("Highest value handled is in the trillions...");
            }
        }
    }
}