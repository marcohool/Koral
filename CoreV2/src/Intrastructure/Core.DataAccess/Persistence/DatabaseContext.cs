using Core.Domain.Common;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.DataAccess.Persistence;

public class DatabaseContext : IdentityDbContext<AppUser>
{
    public DbSet<ClothingItem> ClothingItems { get; set; }

    public DbSet<Upload> Uploads { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (EntityEntry<IAuditedEntity> entry in this.ChangeTracker.Entries<IAuditedEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastUpdatedOn = DateTime.Now;
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                default:
                    break;
            }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
