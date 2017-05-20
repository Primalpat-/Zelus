using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class CharacterZeta
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public string SkillName { get; set; }
        public virtual Character Character { get; set; }
    }
}
