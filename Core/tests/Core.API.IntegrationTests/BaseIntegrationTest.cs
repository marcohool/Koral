using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.API.IntegrationTests.TestHelpers;
using Core.API.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Core.API.IntegrationTests;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebApplicationFactory>
{
    protected BaseIntegrationTest(IntegrationTestWebApplicationFactory factory)
    {
        this.HttpClient = factory.CreateClient();

        this.DbContext = factory
            .Services.CreateScope()
            .ServiceProvider.GetRequiredService<ApplicationDBContext>();

        factory.ResetDatabase().Wait();

        this.ApplicationDBContextHelper = new ApplicationDBContextHelper();
        this.ApplicationDBContextHelper.InitialiseDbForTests(this.DbContext);
    }

    protected HttpClient HttpClient { get; }

    protected ApplicationDBContextHelper ApplicationDBContextHelper { get; }

    protected ApplicationDBContext DbContext { get; }
}
