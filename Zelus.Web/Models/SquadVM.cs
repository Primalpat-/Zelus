namespace Zelus.Web.Models
{
    public class SquadVM
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string SquadName { get; set; }
        public string PlayerName { get; set; }
        public int Damage { get; set; }
        public int PhaseHealth { get; set; }
    }
}