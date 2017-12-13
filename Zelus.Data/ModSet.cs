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
// TargetFrameworkVersion = 4.6
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace Zelus.Data
{

    // ModSets
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class ModSet
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child PlayerMods where [PlayerMods].[SetId] point to this entity (FK_PlayerMods_ModSets)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PlayerMod> PlayerMods { get; set; } // PlayerMods.FK_PlayerMods_ModSets

        public ModSet()
        {
            PlayerMods = new System.Collections.Generic.List<PlayerMod>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
