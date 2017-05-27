using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class PlayerCharacter
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int CharacterId { get; set; }
        public int GearLevel { get; set; }
        public int StarLevel { get; set; }
        public int CharacterLevel { get; set; }
        public string CharacterUrl { get; set; }
        public int NumberOfZetas { get; set; }
        public int PowerLevel { get; set; }
        public virtual Character Character { get; set; }
        public virtual Player Player { get; set; }
    }
}
