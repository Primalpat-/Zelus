// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.5
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace Zelus.Data
{

    // Guilds
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public class GuildConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Guild>
    {
        public GuildConfiguration()
            : this("dbo")
        {
        }

        public GuildConfiguration(string schema)
        {
            ToTable("Guilds", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.AllianceId).HasColumnName(@"AllianceId").HasColumnType("int").IsRequired();
            Property(x => x.SwgohGgId).HasColumnName(@"SwgohGgId").HasColumnType("bigint").IsRequired();
            Property(x => x.SwgohGgUrl).HasColumnName(@"SwgohGgUrl").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.Alliance).WithMany(b => b.Guilds).HasForeignKey(c => c.AllianceId).WillCascadeOnDelete(false); // FK_Guilds_Alliances
        }
    }

}
// </auto-generated>
