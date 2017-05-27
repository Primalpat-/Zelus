namespace Zelus.Web.Models
{
    public class CharacterVM
    {
        public byte[] Portrait { get; set; }
        public int GearLevel { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int StarLevel { get; set; }
        public int CharacterLevel { get; set; }
        public int NumberOfZetas { get; set; }
    }
}