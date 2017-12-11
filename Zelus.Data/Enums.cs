﻿
// ------------------------------------------------------------------------------------------------
// <auto-generated>
//  This code was generated by a C# code generator
//  Generated at 12/1/2017 7:01:46 PM
//
//  Warning: Do not make changes directly to this file; they will get overwritten on the next
//  code generation.
// </auto-generated>
// ------------------------------------------------------------------------------------------------
    
namespace Zelus.Data
{

  /// <summary>
  /// Shapes and slots of a mod
  /// </summary>
  public enum ModSlots : int
  {
    // Database information:
    //  ConnectionStringKey: ZelusContext; Table: [dbo].[ModSlots]; IdColumn: [Id]; NameColumn: [Name]; DescriptionColumn: [Name]
    //  ConnectionString: Data Source=InntecDevServer.cloudapp.net,31337;Initial Catalog=GRA;Persist Security Info=True;User ID=GRAUser;Password=GRA@ccess123;MultipleActiveResultSets=True
 
		/// <summary>
		/// Undefined (not mapped in database)
		/// </summary>
		Undefined = 0,
		Square = 1,
		Arrow = 2,
		Diamond = 3,
		Triangle = 4,
		Circle = 5,
		Cross = 6,

  }
    
  /// <summary>
  /// Set bonus type of a mod
  /// </summary>
  public enum ModSets : int
  {
    // Database information:
    //  ConnectionStringKey: ZelusContext; Table: [dbo].[ModSets]; IdColumn: [Id]; NameColumn: [Name]; DescriptionColumn: [Name]
    //  ConnectionString: Data Source=InntecDevServer.cloudapp.net,31337;Initial Catalog=GRA;Persist Security Info=True;User ID=GRAUser;Password=GRA@ccess123;MultipleActiveResultSets=True
 
		/// <summary>
		/// Undefined (not mapped in database)
		/// </summary>
		Undefined = 0,
		Health = 1,
		Defense = 2,
		Crit_Damage = 3,
		Crit_Chance = 4,
		Tenacity = 5,
		Offense = 6,
		Potency = 7,
		Speed = 8,

  }
    
  /// <summary>
  /// Stat bonus type of a mod
  /// </summary>
  public enum ModStatTypes : int
  {
    // Database information:
    //  ConnectionStringKey: ZelusContext; Table: [dbo].[ModStatTypes]; IdColumn: [Id]; NameColumn: [Name]; DescriptionColumn: [Name]
    //  ConnectionString: Data Source=InntecDevServer.cloudapp.net,31337;Initial Catalog=GRA;Persist Security Info=True;User ID=GRAUser;Password=GRA@ccess123;MultipleActiveResultSets=True
 
		/// <summary>
		/// Undefined (not mapped in database)
		/// </summary>
		Undefined = 0,
		Offense = 1,
		Defense = 2,
		Speed = 3,
		Tenacity = 4,
		Critical_Chance = 5,
		Health = 6,
		Protection = 7,
		Potency = 8,
		Critical_Damage = 9,
		Accuracy = 10,
		Critical_Avoidence = 11,

  }
    
  /// <summary>
  /// Stat bonus units of a mod
  /// </summary>
  public enum ModStatUnits : int
  {
    // Database information:
    //  ConnectionStringKey: ZelusContext; Table: [dbo].[ModStatUnits]; IdColumn: [Id]; NameColumn: [Name]; DescriptionColumn: [Name]
    //  ConnectionString: Data Source=InntecDevServer.cloudapp.net,31337;Initial Catalog=GRA;Persist Security Info=True;User ID=GRAUser;Password=GRA@ccess123;MultipleActiveResultSets=True
 
		/// <summary>
		/// Undefined (not mapped in database)
		/// </summary>
		Undefined = 0,
		Flat = 1,
		Percentage = 2,

  }
    
}