using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zelus.Web.Models.Views.TerritoryWar
{
    public class PlayerCharacterVM
    {
        public int PlayerId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Stars { get; set; }
        public int Gear { get; set; }
        public long Power { get; set; }
    }
}