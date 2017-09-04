using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zelus.Web.Models.Extensions
{
    public static class UrlExtensions
    {
        public static bool IsSimilarTo(this string url, string urlToCompare)
        {
            var uri1 = new Uri(url);
            var uri2 = new Uri(urlToCompare);

            var result = Uri.Compare(uri1, uri2, UriComponents.Host | UriComponents.PathAndQuery,
                UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase);

            return result == 0;
        }
    }
}