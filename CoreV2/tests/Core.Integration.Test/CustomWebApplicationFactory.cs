using Core.DataAccess.Persistence;
using Core.Integration.Test.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Testcontainers.MsSql;

namespace Core.Integration.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer msSqlContainer;
    private Respawner? respawner;

    public CustomWebApplicationFactory()
    {
        this.msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ServiceDescriptor? descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<DatabaseContext>)
            );

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(
                    this.msSqlContainer.GetConnectionString(),
                    config => config.MigrationsAssembly("Core.DataAccess")
                );
            });
        });

        builder.ConfigureTestServices(services =>
        {
            services
                .AddAuthentication(TestAuthHandler.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    TestAuthHandler.AuthenticationScheme,
                    options => { }
                );

            AuthorizationPolicy defaultPolicy = new AuthorizationPolicyBuilder(
                TestAuthHandler.AuthenticationScheme
            )
                .RequireAuthenticatedUser()
                .Build();

            services.AddAuthorizationBuilder().SetDefaultPolicy(defaultPolicy);
        });
    }

    public async Task ResetDatabase()
    {
        if (this.respawner is not null)
        {
            await this.respawner.ResetAsync(this.msSqlContainer.GetConnectionString());
        }
    }

    public async Task InitializeAsync()
    {
        await this.msSqlContainer.StartAsync();

        await this
            .Services.CreateScope()
            .ServiceProvider.GetRequiredService<DatabaseContext>()
            .Database.MigrateAsync();

        this.respawner = await Respawner.CreateAsync(this.msSqlContainer.GetConnectionString());
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await this.msSqlContainer.StopAsync();
    }
}
