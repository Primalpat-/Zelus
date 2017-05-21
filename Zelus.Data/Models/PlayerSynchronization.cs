using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class PlayerSynchronization
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public System.DateTime Timestamp { get; set; }
        public virtual Player Player { get; set; }
    }
}
