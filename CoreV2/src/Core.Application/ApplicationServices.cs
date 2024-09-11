using Core.Application.MappingProfiles;
using Core.Application.Services;
using Core.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddServices();
        services.RegisterAutoMappers();

        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IImageStorageService, ImageStorageService>();
        services.AddScoped<IClothingItemService, ClothingItemService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<IUploadService, UploadService>();
        services.AddScoped<IClothingItemParser, JsonClothingItemParser>();
    }

    private static void RegisterAutoMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ClothingItemProfile));
        services.AddAutoMapper(typeof(UserProfile));
        services.AddAutoMapper(typeof(UploadProfile));
    }
}
