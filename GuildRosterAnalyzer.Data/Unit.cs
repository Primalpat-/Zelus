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


namespace GuildRosterAnalyzer.Data
{

    // Units
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public class Unit
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 500)
        public string BaseId { get; set; } // BaseId (length: 500)
        public string Url { get; set; } // Url (length: 500)
        public string Image { get; set; } // Image (length: 500)
        public long? Power { get; set; } // Power
        public string Description { get; set; } // Description
        public int? CombatType { get; set; } // CombatType
    }

}
// </auto-generated>
