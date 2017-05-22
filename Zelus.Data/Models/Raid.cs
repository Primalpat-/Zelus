using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class Raid
    {
        public Raid()
        {
            this.RaidPhases = new List<RaidPhas>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Subtext { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<RaidPhas> RaidPhases { get; set; }
    }
}
