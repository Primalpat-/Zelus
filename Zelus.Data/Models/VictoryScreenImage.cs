using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class VictoryScreenImage
    {
        public VictoryScreenImage()
        {
            this.Squads = new List<Squad>();
        }

        public int Id { get; set; }
        public string Path { get; set; }
        public virtual ICollection<Squad> Squads { get; set; }
    }
}
