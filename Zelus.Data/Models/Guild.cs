using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class Guild
    {
        public Guild()
        {
            this.GuildSynchronizations = new List<GuildSynchronization>();
            this.Players = new List<Player>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual ICollection<GuildSynchronization> GuildSynchronizations { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
