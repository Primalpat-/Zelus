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

    // Units
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class Unit
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 500)
        public string BaseId { get; set; } // BaseId (length: 500)
        public string Url { get; set; } // Url (length: 500)
        public string Image { get; set; } // Image (length: 500)
        public byte[] LocalImage { get; set; } // LocalImage
        public long? Power { get; set; } // Power
        public string Description { get; set; } // Description
        public int? CombatType { get; set; } // CombatType

        // Reverse navigation

        /// <summary>
        /// Child CharacterSynergies where [CharacterSynergies].[PrimaryCharacterId] point to this entity (FK_CharacterSynergies_Units)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CharacterSynergy> PrimaryCharacter { get; set; } // CharacterSynergies.FK_CharacterSynergies_Units
        /// <summary>
        /// Child CharacterSynergies where [CharacterSynergies].[SynergyCharacterId] point to this entity (FK_CharacterSynergies_Units1)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CharacterSynergy> SynergyCharacter { get; set; } // CharacterSynergies.FK_CharacterSynergies_Units1
        /// <summary>
        /// Child PlayerCharacters where [PlayerCharacters].[UnitId] point to this entity (FK_PlayerCharacters_Units)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PlayerCharacter> PlayerCharacters { get; set; } // PlayerCharacters.FK_PlayerCharacters_Units

        public Unit()
        {
            PrimaryCharacter = new System.Collections.Generic.List<CharacterSynergy>();
            SynergyCharacter = new System.Collections.Generic.List<CharacterSynergy>();
            PlayerCharacters = new System.Collections.Generic.List<PlayerCharacter>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
