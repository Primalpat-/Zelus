using System.Linq;
using Z.Core.Extensions;

namespace Zelus.Web.Helpers
{
    public static class ReduceToDigitsExtension
    {
        public static int ReduceToDigits(this string input)
        {
            return new string(input.Where(char.IsDigit).ToArray()).ToInt32();
        }
    }
}