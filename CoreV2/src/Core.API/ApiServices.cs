using System.Text;
using Core.Application;
using Core.Application.Configuration;
using Core.DataAccess;
using Core.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Core.API;

public static class ApiServices
{
    public static void ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSwagger();

        services.AddDomainServices().AddApplicationServices().AddDataAccessServices(configuration);

        services
            .AddOptions<CloudinaryConfiguration>()
            .BindConfiguration("Cloudinary")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<ImageOptions>()
            .BindConfiguration("Image")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<JwtOptions>()
            .BindConfiguration("JWT")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<KoralMatchConfiguration>()
            .BindConfiguration("KoralMatch")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<MatchingConfiguration>()
            .BindConfiguration("Matching")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddJwt(
            services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value
        );
    }

    private static void AddJwt(this IServiceCollection services, JwtOptions jwtOptions)
    {
        byte[] key = Encoding.ASCII.GetBytes(jwtOptions.SigningKey);

        // TODO: Configure secure JWT options
        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme =
                    x.DefaultChallengeScheme =
                    x.DefaultForbidScheme =
                    x.DefaultScheme =
                    x.DefaultSignInScheme =
                    x.DefaultSignOutScheme =
                        JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                };
            });

        services.AddAuthorization(options =>
        {
            AuthorizationPolicy defaultPolicy = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme
            )
                .RequireAuthenticatedUser()
                .Build();

            options.DefaultPolicy = defaultPolicy;
        });
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme (Example: 'Bearer YOUR_TOKEN')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                }
            );
            s.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );
        });
    }
}
