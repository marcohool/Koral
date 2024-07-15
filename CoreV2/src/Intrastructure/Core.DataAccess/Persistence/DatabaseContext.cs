using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Persistence;

public class DatabaseContext : IdentityDbContext<AppUser>
{
    public DbSet<ClothingItem> ClothingItems { get; set; }

    public DbSet<Upload> Uploads { get; set; }
}
