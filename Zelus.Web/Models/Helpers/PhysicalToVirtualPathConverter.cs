using System;
using System.Web;
using Z.Core.Extensions;

namespace Zelus.Web.Models.Helpers
{
    public static class PhysicalToVirtualPathConverter
    {
        public static string ToVirtual(this string fullServerPath)
        {
            if (fullServerPath.IsNullOrEmpty())
                return @"~\Content\Images\victory-screen-placeholder.jpg";

            return @"~\" + fullServerPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
        }
    }
}