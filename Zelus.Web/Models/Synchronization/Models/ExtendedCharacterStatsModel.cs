namespace Zelus.Web.Models.Synchronization.Models
{
    public class ExtendedCharacterStatsModel
    {
        public ExtendedCharacterStatsModel()
        {
            NumberOfZetas = 0;
            PowerLevel = 0;
        }

        public int NumberOfZetas { get; set; }
        public int PowerLevel { get; set; }
    }
}