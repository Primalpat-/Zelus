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

    // Players
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public class Player
    {
        public int Id { get; set; } // Id (Primary key)
        public int GuildId { get; set; } // GuildId
        public string InGameName { get; set; } // InGameName (length: 50)
        public string SwgohGgName { get; set; } // SwgohGgName (length: 50)
        public string SwgohGgUrl { get; set; } // SwgohGgUrl (length: 100)

        // Foreign keys

        /// <summary>
        /// Parent Guild pointed by [Players].([GuildId]) (FK_Players_Guilds)
        /// </summary>
        public virtual Guild Guild { get; set; } // FK_Players_Guilds
    }

}
// </auto-generated>