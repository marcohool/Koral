using Core.Application;
using Core.DataAccess;
using Core.Domain;

namespace Core.API.Configuration;

public static class ServiceConfiguration
{
    public static void ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSwagger();
        services.AddJwt(configuration);

        services.AddDomainServices().AddApplicationServices().AddDataAccessServices(configuration);

        services
            .AddOptions<CloudinaryConfiguration>()
            .BindConfiguration("CloudinaryConfiguration")
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
