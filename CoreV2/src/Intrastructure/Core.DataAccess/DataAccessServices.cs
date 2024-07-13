using Microsoft.Extensions.DependencyInjection;

namespace Core.DataAccess;

public static class DataAccessServices
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        return services;
    }
}