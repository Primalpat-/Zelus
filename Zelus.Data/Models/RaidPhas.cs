using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class RaidPhas
    {
        public RaidPhas()
        {
            this.Squads = new List<Squad>();
        }

        public int Id { get; set; }
        public int RaidId { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public virtual Raid Raid { get; set; }
        public virtual ICollection<Squad> Squads { get; set; }
    }
}
