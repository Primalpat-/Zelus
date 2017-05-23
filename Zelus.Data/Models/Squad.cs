using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class Squad
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TargetPhaseId { get; set; }
        public int Damage { get; set; }
        public Nullable<int> VictoryScreenImageId { get; set; }
        public int Member1Id { get; set; }
        public Nullable<int> Member2Id { get; set; }
        public Nullable<int> Member3Id { get; set; }
        public Nullable<int> Member4Id { get; set; }
        public Nullable<int> Member5Id { get; set; }
        public string Notes { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public System.DateTime Timestamp { get; set; }
        public virtual PlayerCharacter PlayerCharacter { get; set; }
        public virtual PlayerCharacter PlayerCharacter1 { get; set; }
        public virtual PlayerCharacter PlayerCharacter2 { get; set; }
        public virtual PlayerCharacter PlayerCharacter3 { get; set; }
        public virtual PlayerCharacter PlayerCharacter4 { get; set; }
        public virtual RaidPhas RaidPhas { get; set; }
        public virtual VictoryScreenImage VictoryScreenImage { get; set; }
    }
}
