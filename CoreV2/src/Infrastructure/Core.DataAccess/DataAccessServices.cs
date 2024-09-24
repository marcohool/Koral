using Core.DataAccess.Identity;
using Core.DataAccess.Persistence;
using Core.DataAccess.Persistence.Configurations;
using Core.DataAccess.Repositories;
using Core.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DataAccess;

public static class DataAccessServices
{
    public static void AddDataAccessServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDatabase(configuration);
        services.AddIdentity();
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUploadRepository, UploadRepository>();
        services.AddScoped<IClothingItemRepository, ClothingItemRepository>();
        services.AddScoped<IItemMatchRepository, ItemMatchRepository>();
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        DatabaseConfiguration? databaseConfig = configuration
            .GetRequiredSection("Database")
            .Get<DatabaseConfiguration>();

        if (databaseConfig == null)
        {
            throw new InvalidOperationException("Database configuration is missing or invalid.");
        }

        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(databaseConfig.ConnectionString)
        );
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services
            .AddDefaultIdentity<ApplicationUser>(options =>
                options.SignIn.RequireConfirmedAccount = true
            )
            .AddEntityFrameworkStores<DatabaseContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;

            options.SignIn.RequireConfirmedEmail = false;
        });
    }
}
