using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zelus.Data.Models.Mapping
{
    public class PlayerCharacterMap : EntityTypeConfiguration<PlayerCharacter>
    {
        public PlayerCharacterMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CharacterUrl)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("PlayerCharacters", "db_owner");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PlayerId).HasColumnName("PlayerId");
            this.Property(t => t.CharacterId).HasColumnName("CharacterId");
            this.Property(t => t.GearLevel).HasColumnName("GearLevel");
            this.Property(t => t.StarLevel).HasColumnName("StarLevel");
            this.Property(t => t.CharacterLevel).HasColumnName("CharacterLevel");
            this.Property(t => t.CharacterUrl).HasColumnName("CharacterUrl");
            this.Property(t => t.NumberOfZetas).HasColumnName("NumberOfZetas");
            this.Property(t => t.PowerLevel).HasColumnName("PowerLevel");

            // Relationships
            this.HasRequired(t => t.Character)
                .WithMany(t => t.PlayerCharacters)
                .HasForeignKey(d => d.CharacterId);
            this.HasRequired(t => t.Player)
                .WithMany(t => t.PlayerCharacters)
                .HasForeignKey(d => d.PlayerId);

        }
    }
}
