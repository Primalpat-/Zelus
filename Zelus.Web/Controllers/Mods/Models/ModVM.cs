using Zelus.Data;

namespace Zelus.Web.Models.Views.Mods
{
    public class ModVM
    {
        public bool IsInModSet { get; set; }
        public bool ShowCheckbox { get; set; }

        public int Id { get; set; }

        public int Pips { get; set; }

        public ModSets Set { get; set; }

        public ModSlots Slot { get; set; }
        

        public string CharacterName { get; set; }

        public string CharacterUrl { get; set; }

        public string CharacterImg { get; set; }


        public string Primary { get; set; }

        public string Secondary1 { get; set; }

        public string Secondary2 { get; set; }

        public string Secondary3 { get; set; }

        public string Secondary4 { get; set; }
    }
}