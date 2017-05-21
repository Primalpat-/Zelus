using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Zelus.Data.Models.Mapping;

namespace Zelus.Data.Models
{
    public partial class ZelusContext : DbContext
    {
        static ZelusContext()
        {
            Database.SetInitializer<ZelusContext>(null);
        }

        public ZelusContext()
            : base("Name=ZelusContext")
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterZeta> CharacterZetas { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<GuildSynchronization> GuildSynchronizations { get; set; }
        public DbSet<PlayerCharacter> PlayerCharacters { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerSynchronization> PlayerSynchronizations { get; set; }
        public DbSet<RaidPhas> RaidPhases { get; set; }
        public DbSet<Raid> Raids { get; set; }
        public DbSet<Squad> Squads { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CharacterMap());
            modelBuilder.Configurations.Add(new CharacterZetaMap());
            modelBuilder.Configurations.Add(new GuildMap());
            modelBuilder.Configurations.Add(new GuildSynchronizationMap());
            modelBuilder.Configurations.Add(new PlayerCharacterMap());
            modelBuilder.Configurations.Add(new PlayerMap());
            modelBuilder.Configurations.Add(new PlayerSynchronizationMap());
            modelBuilder.Configurations.Add(new RaidPhasMap());
            modelBuilder.Configurations.Add(new RaidMap());
            modelBuilder.Configurations.Add(new SquadMap());
        }
    }
}
