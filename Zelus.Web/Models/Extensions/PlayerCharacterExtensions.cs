using Zelus.Data.Models;

namespace Zelus.Web.Models.Extensions
{
    public static class PlayerCharacterExtensions
    {
        public static CharacterVM ToCharacterVM(this PlayerCharacter playerCharacter)
        {
            return new CharacterVM
            {
                Portrait = playerCharacter.Character.Portrait,
                GearLevel = playerCharacter.GearLevel,
                Url = playerCharacter.CharacterUrl,
                Name = playerCharacter.Character.Name,
                StarLevel = playerCharacter.StarLevel,
                CharacterLevel = playerCharacter.CharacterLevel,
                NumberOfZetas = playerCharacter.NumberOfZetas
            };
        }

        public static SquadCharacter ToSquadCharacter(this PlayerCharacter playerCharacter)
        {
            return new SquadCharacter
            {
                CharacterId = playerCharacter.CharacterId,
                GearLevel = playerCharacter.GearLevel,
                StarLevel = playerCharacter.StarLevel,
                CharacterLevel = playerCharacter.CharacterLevel,
                CharacterUrl = playerCharacter.CharacterUrl,
                NumberOfZetas = playerCharacter.NumberOfZetas
            };
        }
    }
}