using Microsoft.EntityFrameworkCore;

namespace Core.API.Models;

/// <summary>
/// The <see cref="ApplicationDBContext"/> class.
/// </summary>
/// <remarks>
/// Initialises a new instance of the <see cref="ApplicationDBContext"/> class.
/// </remarks>
/// <param name="options"></param>
public class ApplicationDBContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// The <see cref="ClothingItems"/> property represeting the clothing items in the database.
    /// </summary>
    public DbSet<ClothingItem> ClothingItems { get; set; }
}
