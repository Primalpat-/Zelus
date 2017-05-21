using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zelus.Data.Models.Mapping
{
    public class RaidPhasMap : EntityTypeConfiguration<RaidPhas>
    {
        public RaidPhasMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("RaidPhases", "db_owner");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RaidId).HasColumnName("RaidId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Health).HasColumnName("Health");

            // Relationships
            this.HasRequired(t => t.Raid)
                .WithMany(t => t.RaidPhases)
                .HasForeignKey(d => d.RaidId);

        }
    }
}
