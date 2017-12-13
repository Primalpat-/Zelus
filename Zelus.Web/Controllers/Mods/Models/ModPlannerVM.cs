using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zelus.Web.Models.Views.Mods
{
    public class ModPlannerVM
    {
        public int PlayerId { get; set; }
        public string LastSyncHumanized { get; set; }
        public DateTime LastSyncDateTime { get; set; }
    }
}