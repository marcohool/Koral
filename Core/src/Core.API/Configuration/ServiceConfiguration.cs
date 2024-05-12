using System.Text;
using Core.API.Models;
using Core.API.Repository;
using Core.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        services.AddSwaggerGen();

        // Add DB Context
        services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        // Add Identity
        services
            .AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 12;
            })
            .AddEntityFrameworkStores<ApplicationDBContext>();

        // Add Authentication
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    options.DefaultChallengeScheme =
                    options.DefaultForbidScheme =
                    options.DefaultScheme =
                    options.DefaultSignInScheme =
                    options.DefaultSignOutScheme =
                        JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]!)
                    ),
                };
            });

        // Register services for Dependency Injection
        services
            .AddScoped<IClothingItemService, ClothingItemService>()
            .AddScoped<IClothingItemRepository, ClothingItemRepository>();

        services.AddControllers();

        return services;
    }
}
