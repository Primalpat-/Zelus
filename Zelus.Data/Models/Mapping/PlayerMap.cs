using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zelus.Data.Models.Mapping
{
    public class PlayerMap : EntityTypeConfiguration<Player>
    {
        public PlayerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.CollectionUrl)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Players", "db_owner");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.GuildId).HasColumnName("GuildId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CollectionUrl).HasColumnName("CollectionUrl");
            this.Property(t => t.PlayerLevel).HasColumnName("PlayerLevel");
            this.Property(t => t.ArenaRank).HasColumnName("ArenaRank");
            this.Property(t => t.ArenaAverage).HasColumnName("ArenaAverage");
            this.Property(t => t.CollectionScore).HasColumnName("CollectionScore");
            this.Property(t => t.GuildCurrency).HasColumnName("GuildCurrency");

            // Relationships
            this.HasRequired(t => t.Guild)
                .WithMany(t => t.Players)
                .HasForeignKey(d => d.GuildId);

        }
    }
}
