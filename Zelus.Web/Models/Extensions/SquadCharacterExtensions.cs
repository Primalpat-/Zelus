using Zelus.Data.Models;

namespace Zelus.Web.Models.Extensions
{
    public static class SquadCharacterExtensions
    {
        public static CharacterVM ToCharacterVM(this SquadCharacter squadCharacter)
        {
            return new CharacterVM
            {
                Portrait = squadCharacter.Character.Portrait,
                GearLevel = squadCharacter.GearLevel,
                Url = squadCharacter.CharacterUrl,
                Name = squadCharacter.Character.Name,
                StarLevel = squadCharacter.StarLevel,
                CharacterLevel = squadCharacter.CharacterLevel,
                NumberOfZetas = squadCharacter.NumberOfZetas
            };
        }
    }
}