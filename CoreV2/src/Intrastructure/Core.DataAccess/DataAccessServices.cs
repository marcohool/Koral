using Core.DataAccess.Persistence;
using Core.DataAccess.Persistence.Configurations;
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

        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        // services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        // services.AddScoped<ITodoListRepository, TodoListRepository>();
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        DatabaseConfiguration databaseConfig =
            configuration.GetRequiredSection("Database").Get<DatabaseConfiguration>()
            ?? throw new InvalidOperationException("Database configuration is missing or invalid.");

        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(databaseConfig.ConnectionString)
        );
    }
}
