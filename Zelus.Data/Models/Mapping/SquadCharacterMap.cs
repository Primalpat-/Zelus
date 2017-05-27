using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zelus.Data.Models.Mapping
{
    public class SquadCharacterMap : EntityTypeConfiguration<SquadCharacter>
    {
        public SquadCharacterMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CharacterUrl)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("SquadCharacters", "db_owner");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CharacterId).HasColumnName("CharacterId");
            this.Property(t => t.GearLevel).HasColumnName("GearLevel");
            this.Property(t => t.StarLevel).HasColumnName("StarLevel");
            this.Property(t => t.CharacterLevel).HasColumnName("CharacterLevel");
            this.Property(t => t.CharacterUrl).HasColumnName("CharacterUrl");
            this.Property(t => t.NumberOfZetas).HasColumnName("NumberOfZetas");

            // Relationships
            this.HasRequired(t => t.Character)
                .WithMany(t => t.SquadCharacters)
                .HasForeignKey(d => d.CharacterId);
        }
    }
}
