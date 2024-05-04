using Core.API.Services;
using Microsoft.OpenApi.Models;

namespace Core.API.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSwaggerGen();

        // Register services for Dependency Injection
        services.AddScoped<IClothingItemService, ClothingItemService>();

        services.AddControllers();

        return services;
    }
}
