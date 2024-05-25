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
    }
}
