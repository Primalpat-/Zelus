using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zelus.Data.Models.Mapping
{
    public class GuildSynchronizationMap : EntityTypeConfiguration<GuildSynchronization>
    {
        public GuildSynchronizationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("GuildSynchronizations", "db_owner");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.GuildId).HasColumnName("GuildId");
            this.Property(t => t.Timestamp).HasColumnName("Timestamp");

            // Relationships
            this.HasRequired(t => t.Guild)
                .WithMany(t => t.GuildSynchronizations)
                .HasForeignKey(d => d.GuildId);

        }
    }
}
