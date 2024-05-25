using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.API.Models;

/// <summary>
/// The <see cref="ApplicationDBContext"/> class.
/// </summary>
/// <remarks>
/// Initialises a new instance of the <see cref="ApplicationDBContext"/> class.
/// </remarks>
/// <param name="options">The options used to configure the database connection.</param>
public class ApplicationDBContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    /// <summary>
    /// The <see cref="ClothingItems"/> property represeting the clothing items in the database.
    /// </summary>
    public DbSet<ClothingItem> ClothingItems { get; set; }

    /// <summary>
    /// The <see cref="ImageUploads"/> property representing the image uploads in the database.
    /// </summary>
    public DbSet<ImageUpload> ImageUploads { get; set; }

    /// <summary>
    /// The <see cref="OnModelCreating"/> method.
    /// </summary>
    /// <param name="builder">Instance of <see cref="ModelBuilder"/>.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ClothingItem>().ToTable("ClothingItems");

        List<IdentityRole> roles =
        [
            new IdentityRole
            {
                Id = "bd91e2d5-8e11-4649-92b5-25715dfb402c",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "1e10f5c3-54a6-4dc4-b968-3f2002304da5",
                Name = "User",
                NormalizedName = "USER"
            }
        ];

        builder.Entity<IdentityRole>().HasData(roles);
    }
}
