using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zelus.Data.Models.Mapping
{
    public class SquadMap : EntityTypeConfiguration<Squad>
    {
        public SquadMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Squads", "db_owner");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.TargetPhaseId).HasColumnName("TargetPhaseId");
            this.Property(t => t.Damage).HasColumnName("Damage");
            this.Property(t => t.VictoryScreenImageId).HasColumnName("VictoryScreenImageId");
            this.Property(t => t.Member1Id).HasColumnName("Member1Id");
            this.Property(t => t.Member2Id).HasColumnName("Member2Id");
            this.Property(t => t.Member3Id).HasColumnName("Member3Id");
            this.Property(t => t.Member4Id).HasColumnName("Member4Id");
            this.Property(t => t.Member5Id).HasColumnName("Member5Id");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.Upvotes).HasColumnName("Upvotes");
            this.Property(t => t.Downvotes).HasColumnName("Downvotes");
            this.Property(t => t.Timestamp).HasColumnName("Timestamp");

            // Relationships
            this.HasRequired(t => t.PlayerCharacter)
                .WithMany(t => t.Squads)
                .HasForeignKey(d => d.Member1Id);
            this.HasOptional(t => t.PlayerCharacter1)
                .WithMany(t => t.Squads1)
                .HasForeignKey(d => d.Member2Id);
            this.HasOptional(t => t.PlayerCharacter2)
                .WithMany(t => t.Squads2)
                .HasForeignKey(d => d.Member3Id);
            this.HasOptional(t => t.PlayerCharacter3)
                .WithMany(t => t.Squads3)
                .HasForeignKey(d => d.Member4Id);
            this.HasOptional(t => t.PlayerCharacter4)
                .WithMany(t => t.Squads4)
                .HasForeignKey(d => d.Member5Id);
            this.HasRequired(t => t.RaidPhas)
                .WithMany(t => t.Squads)
                .HasForeignKey(d => d.TargetPhaseId);
            this.HasOptional(t => t.VictoryScreenImage)
                .WithMany(t => t.Squads)
                .HasForeignKey(d => d.VictoryScreenImageId);

        }
    }
}
