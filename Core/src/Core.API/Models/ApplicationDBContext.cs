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
}
