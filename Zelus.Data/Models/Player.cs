using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class Player
    {
        public Player()
        {
            this.PlayerCharacters = new List<PlayerCharacter>();
            this.PlayerSynchronizations = new List<PlayerSynchronization>();
            this.Squads = new List<Squad>();
        }

        public int Id { get; set; }
        public int GuildId { get; set; }
        public string Name { get; set; }
        public string CollectionUrl { get; set; }
        public int PlayerLevel { get; set; }
        public virtual Guild Guild { get; set; }
        public virtual ICollection<PlayerCharacter> PlayerCharacters { get; set; }
        public virtual ICollection<PlayerSynchronization> PlayerSynchronizations { get; set; }
        public virtual ICollection<Squad> Squads { get; set; }
    }
}
