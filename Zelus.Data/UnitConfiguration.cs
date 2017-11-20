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

    // Units
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public class UnitConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Unit>
    {
        public UnitConfiguration()
            : this("dbo")
        {
        }

        public UnitConfiguration(string schema)
        {
            ToTable("Units", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.BaseId).HasColumnName(@"BaseId").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.Url).HasColumnName(@"Url").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.Image).HasColumnName(@"Image").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.Power).HasColumnName(@"Power").HasColumnType("bigint").IsOptional();
            Property(x => x.Description).HasColumnName(@"Description").HasColumnType("varchar(max)").IsOptional().IsUnicode(false);
            Property(x => x.CombatType).HasColumnName(@"CombatType").HasColumnType("int").IsOptional();
        }
    }

}
// </auto-generated>