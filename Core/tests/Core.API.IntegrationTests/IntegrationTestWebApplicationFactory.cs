using Core.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Testcontainers.MsSql;

namespace Core.API.IntegrationTests;

public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer msSqlContainer;
    private Respawner respawner;

    public IntegrationTestWebApplicationFactory()
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
                d.ServiceType == typeof(DbContextOptions<ApplicationDBContext>)
            );

            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(
                    this.msSqlContainer.GetConnectionString(),
                    config => config.MigrationsAssembly("Core.API")
                );
            });
        });
    }

    public async Task InitializeAsync()
    {
        await this.msSqlContainer.StartAsync();

        await this
            .Services.CreateScope()
            .ServiceProvider.GetRequiredService<ApplicationDBContext>()
            .Database.MigrateAsync();

        this.respawner = await Respawner.CreateAsync(this.msSqlContainer.GetConnectionString());
    }

    public new Task DisposeAsync() => this.msSqlContainer.StopAsync();

    public async Task ResetDatabase() =>
        await this.respawner.ResetAsync(this.msSqlContainer.GetConnectionString());
}
