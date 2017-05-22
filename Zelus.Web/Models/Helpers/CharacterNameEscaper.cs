using System.Text.RegularExpressions;

namespace Zelus.Web.Models.Helpers
{
    public static class CharacterNameEscaper
    {
        public static string Escape(this string name)
        {
            var newName = Regex.Replace(name, @"\\", @"\\\");
            return newName;
        }
    }
}