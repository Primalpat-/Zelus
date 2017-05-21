namespace Zelus.Web.Models.Helpers
{
    public static class StarLevelConverter
    {
        public static string IsActive(this int level, int star)
        {
            if (star > level)
                return "star-inactive";

            return "";
        }
    }
}