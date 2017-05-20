using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zelus.Data.Models.Mapping
{
    public class CharacterZetaMap : EntityTypeConfiguration<CharacterZeta>
    {
        public CharacterZetaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SkillName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CharacterZetas", "db_owner");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CharacterId).HasColumnName("CharacterId");
            this.Property(t => t.SkillName).HasColumnName("SkillName");

            // Relationships
            this.HasRequired(t => t.Character)
                .WithMany(t => t.CharacterZetas)
                .HasForeignKey(d => d.CharacterId);
        }
    }
}
