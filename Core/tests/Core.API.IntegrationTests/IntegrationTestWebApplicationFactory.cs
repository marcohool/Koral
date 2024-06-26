﻿using Core.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Testcontainers.MsSql;

namespace Core.API.IntegrationTests;

public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer msSqlContainer;
    private Respawner respawner;

    private readonly string tempWebRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

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

        builder.UseWebRoot(this.tempWebRoot);
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

    public new async Task DisposeAsync()
    {
        if (Directory.Exists(this.tempWebRoot))
            Directory.Delete(this.tempWebRoot, true);

        await this.msSqlContainer.StopAsync();
    }

    public async Task ResetDatabase() =>
        await this.respawner.ResetAsync(this.msSqlContainer.GetConnectionString());
}
