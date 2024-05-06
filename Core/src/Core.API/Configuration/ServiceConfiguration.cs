using Core.API.Models;
using Core.API.Repository;
using Core.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Core.API.Configuration;

/// <summary>
/// The <see cref="ServiceConfiguration"/> class.
/// </summary>
public static class ServiceConfiguration
{
    /// <summary>
    /// Congifures the services for the application.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">Instance of <see cref="IConfiguration"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Add DB Context
        services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddSwaggerGen();

        // Register services for Dependency Injection
        services
            .AddScoped<IClothingItemService, ClothingItemService>()
            .AddScoped<IClothingItemRepository, ClothingItemRepository>();

        services.AddControllers();

        return services;
    }
}
