using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class SquadCharacter
    {
        public SquadCharacter()
        {
            this.Squads = new List<Squad>();
            this.Squads1 = new List<Squad>();
            this.Squads2 = new List<Squad>();
            this.Squads3 = new List<Squad>();
            this.Squads4 = new List<Squad>();
        }

        public int Id { get; set; }
        public int CharacterId { get; set; }
        public int GearLevel { get; set; }
        public int StarLevel { get; set; }
        public int CharacterLevel { get; set; }
        public string CharacterUrl { get; set; }
        public int NumberOfZetas { get; set; }
        public virtual ICollection<Squad> Squads { get; set; }
        public virtual ICollection<Squad> Squads1 { get; set; }
        public virtual ICollection<Squad> Squads2 { get; set; }
        public virtual ICollection<Squad> Squads3 { get; set; }
        public virtual ICollection<Squad> Squads4 { get; set; }
        public virtual Character Character { get; set; }
    }
}
