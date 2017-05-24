using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zelus.Data.Models.Mapping
{
    public class VictoryScreenImageMap : EntityTypeConfiguration<VictoryScreenImage>
    {
        public VictoryScreenImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Path)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("VictoryScreenImages", "db_owner");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Path).HasColumnName("Path");
        }
    }
}
