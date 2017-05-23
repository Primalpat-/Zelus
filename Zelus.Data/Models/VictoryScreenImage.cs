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
        public byte[] Data { get; set; }
        public virtual ICollection<Squad> Squads { get; set; }
        public virtual VictoryScreenImage VictoryScreenImages1 { get; set; }
        public virtual VictoryScreenImage VictoryScreenImage1 { get; set; }
    }
}
