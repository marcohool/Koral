using AutoMapper;
using Core.Application.Services;
using Core.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddServices();

        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IImageStorageService, ImageStorageService>();
        services.AddScoped<IClothingItemService, ClothingItemService>();

        services.AddAutoMapper(typeof(Profile));
    }
}
