using System;
using System.Web;

namespace Zelus.Web.Models.Helpers
{
    public static class PhysicalToVirtualPathConverter
    {
        public static string ToVirtual(this string fullServerPath)
        {
            return @"~\" + fullServerPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
        }
    }
}