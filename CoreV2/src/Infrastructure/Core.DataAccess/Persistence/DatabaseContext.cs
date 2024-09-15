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
    public DbSet<ClothingItem> ClothingItems { get; set; }

    public DbSet<Upload> Uploads { get; set; }

    public DbSet<UploadClothingItem> UploadClothingItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder
            .Entity<UploadClothingItem>()
            .HasKey(uci => new { uci.UploadId, uci.ClothingItemId });

        builder
            .Entity<UploadClothingItem>()
            .HasOne(uc => uc.Upload)
            .WithMany(u => u.UploadClothingItems)
            .HasForeignKey(uc => uc.UploadId);

        builder
            .Entity<UploadClothingItem>()
            .HasOne(uc => uc.ClothingItem)
            .WithMany(c => c.UploadClothingItems)
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
