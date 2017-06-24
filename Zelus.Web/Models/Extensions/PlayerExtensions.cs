namespace Zelus.Web.Models.Extensions
{
    public static class PlayerExtensions
    {
        public static string ToCollectionUrl(this string playerUsername)
        {
            return $"https://swgoh.gg/u/{playerUsername.Trim()}/collection/";
        }
    }
}