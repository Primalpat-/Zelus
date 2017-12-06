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

    // ModStatTypes
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class ModStatType
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child PlayerMods where [PlayerMods].[PrimaryTypeId] point to this entity (FK_PlayerMods_ModStatTypes)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PlayerMod> PrimaryType { get; set; } // PlayerMods.FK_PlayerMods_ModStatTypes
        /// <summary>
        /// Child PlayerMods where [PlayerMods].[Secondary1TypeId] point to this entity (FK_PlayerMods_ModStatTypes1)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PlayerMod> Secondary1Type { get; set; } // PlayerMods.FK_PlayerMods_ModStatTypes1
        /// <summary>
        /// Child PlayerMods where [PlayerMods].[Secondary2TypeId] point to this entity (FK_PlayerMods_ModStatTypes2)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PlayerMod> Secondary2Type { get; set; } // PlayerMods.FK_PlayerMods_ModStatTypes2
        /// <summary>
        /// Child PlayerMods where [PlayerMods].[Secondary3TypeId] point to this entity (FK_PlayerMods_ModStatTypes3)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PlayerMod> Secondary3Type { get; set; } // PlayerMods.FK_PlayerMods_ModStatTypes3
        /// <summary>
        /// Child PlayerMods where [PlayerMods].[Secondary4TypeId] point to this entity (FK_PlayerMods_ModStatTypes4)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PlayerMod> Secondary4Type { get; set; } // PlayerMods.FK_PlayerMods_ModStatTypes4

        public ModStatType()
        {
            PrimaryType = new System.Collections.Generic.List<PlayerMod>();
            Secondary1Type = new System.Collections.Generic.List<PlayerMod>();
            Secondary2Type = new System.Collections.Generic.List<PlayerMod>();
            Secondary3Type = new System.Collections.Generic.List<PlayerMod>();
            Secondary4Type = new System.Collections.Generic.List<PlayerMod>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
