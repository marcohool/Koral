using System.Reflection;
using Core.DataAccess.Identity;
using Core.Domain.Common;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.DataAccess.Persistence;

public class DatabaseContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Upload> Uploads { get; set; }

    public DbSet<UploadItem> UploadItems { get; set; }

    public DbSet<ItemMatch> UploadMatches { get; set; }

    public DbSet<ClothingItem> ClothingItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<ItemMatch>().HasKey(uci => new { uci.UploadItemId, uci.ClothingItemId });

        builder
            .Entity<ItemMatch>()
            .HasOne(im => im.UploadItem)
            .WithMany(u => u.ItemMatches)
            .HasForeignKey(im => im.UploadItemId);

        builder
            .Entity<ItemMatch>()
            .HasOne(uc => uc.ClothingItem)
            .WithMany(c => c.ItemMatches)
            .HasForeignKey(uc => uc.ClothingItemId);

        base.OnModelCreating(builder);
    }

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
