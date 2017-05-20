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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CharacterMap());
            modelBuilder.Configurations.Add(new CharacterZetaMap());
            modelBuilder.Configurations.Add(new GuildMap());
            modelBuilder.Configurations.Add(new GuildSynchronizationMap());
            modelBuilder.Configurations.Add(new PlayerCharacterMap());
            modelBuilder.Configurations.Add(new PlayerMap());
        }
    }
}
