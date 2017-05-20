using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class Character
    {
        public Character()
        {
            this.PlayerCharacters = new List<PlayerCharacter>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Portrait { get; set; }
        public virtual ICollection<PlayerCharacter> PlayerCharacters { get; set; }
        public virtual ICollection<CharacterZeta> CharacterZetas { get; set; }
    }
}
