using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class Player
    {
        public Player()
        {
            this.PlayerCharacters = new List<PlayerCharacter>();
        }

        public int Id { get; set; }
        public int GuildId { get; set; }
        public string Name { get; set; }
        public string CollectionUrl { get; set; }
        public int PlayerLevel { get; set; }
        public int ArenaRank { get; set; }
        public int ArenaAverage { get; set; }
        public decimal CollectionScore { get; set; }
        public int GuildCurrency { get; set; }
        public virtual Guild Guild { get; set; }
        public virtual ICollection<PlayerCharacter> PlayerCharacters { get; set; }
    }
}
