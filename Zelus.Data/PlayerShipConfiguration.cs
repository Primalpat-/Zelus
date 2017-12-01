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

    // PlayerShips
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public class PlayerShipConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PlayerShip>
    {
        public PlayerShipConfiguration()
            : this("dbo")
        {
        }

        public PlayerShipConfiguration(string schema)
        {
            ToTable("PlayerShips", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.PlayerId).HasColumnName(@"PlayerId").HasColumnType("int").IsRequired();
            Property(x => x.UnitId).HasColumnName(@"UnitId").HasColumnType("int").IsRequired();
            Property(x => x.Level).HasColumnName(@"Level").HasColumnType("int").IsRequired();
            Property(x => x.Stars).HasColumnName(@"Stars").HasColumnType("int").IsRequired();
            Property(x => x.Power).HasColumnName(@"Power").HasColumnType("bigint").IsRequired();
        }
    }

}
// </auto-generated>