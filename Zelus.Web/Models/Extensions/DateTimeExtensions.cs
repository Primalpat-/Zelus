using System;

namespace Zelus.Web.Models.Extensions
{
    public static class DateTimeExtensions
    {
        public static double DurationSince(this DateTime date)
        {
            return (date - DateTime.UtcNow).TotalMilliseconds;
        }
    }
}