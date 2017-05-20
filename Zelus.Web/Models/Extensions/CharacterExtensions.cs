using System.Linq;
using Zelus.Data.Models;

namespace Zelus.Web.Models.Extensions
{
    public static class CharacterExtensions
    {
        public static bool CanHaveZeta(this Character character, int level)
        {
            return character.CharacterZetas.Any() && level >= 82;
        }
    }
}