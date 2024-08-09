using Core.Application.Services.Interfaces;
using Core.DataAccess.Persistence;
using Core.Integration.Test.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Core.Integration.Test;

public class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory>
{
    protected BaseIntegrationTest(CustomWebApplicationFactory factory)
    {
        this.HttpClient = factory
            .WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                    services.AddSingleton(this.MockImageStorageService.Object)
                )
            )
            .CreateClient();

        this.DbContext = factory
            .Services.CreateScope()
            .ServiceProvider.GetRequiredService<DatabaseContext>();

        factory.ResetDatabase().Wait();

        this.DatabaseContextHelper = new DatabaseContextHelper(this.DbContext);
    }

    protected DatabaseContextHelper DatabaseContextHelper { get; }

    protected HttpClient HttpClient { get; }

    protected DatabaseContext DbContext { get; }

    protected Mock<IImageStorageService> MockImageStorageService { get; } =
        new(MockBehavior.Strict);
}
