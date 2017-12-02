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

    public interface IZelusDbContext : System.IDisposable
    {
        System.Data.Entity.DbSet<Alliance> Alliances { get; set; } // Alliances
        System.Data.Entity.DbSet<Guild> Guilds { get; set; } // Guilds
        System.Data.Entity.DbSet<ModSet> ModSets { get; set; } // ModSets
        System.Data.Entity.DbSet<ModSlot> ModSlots { get; set; } // ModSlots
        System.Data.Entity.DbSet<ModStatType> ModStatTypes { get; set; } // ModStatTypes
        System.Data.Entity.DbSet<ModStatUnit> ModStatUnits { get; set; } // ModStatUnits
        System.Data.Entity.DbSet<Player> Players { get; set; } // Players
        System.Data.Entity.DbSet<PlayerCharacter> PlayerCharacters { get; set; } // PlayerCharacters
        System.Data.Entity.DbSet<PlayerMod> PlayerMods { get; set; } // PlayerMods
        System.Data.Entity.DbSet<PlayerShip> PlayerShips { get; set; } // PlayerShips
        System.Data.Entity.DbSet<Unit> Units { get; set; } // Units

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
        System.Data.Entity.Infrastructure.DbChangeTracker ChangeTracker { get; }
        System.Data.Entity.Infrastructure.DbContextConfiguration Configuration { get; }
        System.Data.Entity.Database Database { get; }
        System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity);
        System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> GetValidationErrors();
        System.Data.Entity.DbSet Set(System.Type entityType);
        System.Data.Entity.DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
    }

}
// </auto-generated>
