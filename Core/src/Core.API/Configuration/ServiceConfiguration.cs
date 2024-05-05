using Core.API.Models;
using Core.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Core.API.Configuration;

public static class ServiceConfiguration
{
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
        services.AddScoped<IClothingItemService, ClothingItemService>();

        services.AddControllers();

        return services;
    }
}
