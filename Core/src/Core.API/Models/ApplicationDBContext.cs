using Microsoft.EntityFrameworkCore;

namespace Core.API.Models;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions options)
        : base(options) { }

    public DbSet<ClothingItem> ClothingItems { get; set; }
}
