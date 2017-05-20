using System;
using System.Collections.Generic;

namespace Zelus.Data.Models
{
    public partial class GuildSynchronization
    {
        public int Id { get; set; }
        public int GuildId { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string Data { get; set; }
        public virtual Guild Guild { get; set; }
    }
}
