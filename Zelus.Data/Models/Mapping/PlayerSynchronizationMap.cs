using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zelus.Data.Models.Mapping
{
    public class PlayerSynchronizationMap : EntityTypeConfiguration<PlayerSynchronization>
    {
        public PlayerSynchronizationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("PlayerSynchronizations", "db_owner");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PlayerId).HasColumnName("PlayerId");
            this.Property(t => t.Timestamp).HasColumnName("Timestamp");

            // Relationships
            this.HasRequired(t => t.Player)
                .WithMany(t => t.PlayerSynchronizations)
                .HasForeignKey(d => d.PlayerId);

        }
    }
}
