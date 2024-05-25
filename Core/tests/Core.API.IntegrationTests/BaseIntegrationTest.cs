using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.API.IntegrationTests.TestHelpers;
using Core.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Sdk;

namespace Core.API.IntegrationTests;

[Collection("IntegrationTest")]
public class BaseIntegrationTest : IClassFixture<IntegrationTestWebApplicationFactory>
{
    protected BaseIntegrationTest(IntegrationTestWebApplicationFactory factory)
    {
        this.HttpClient = factory.CreateClient();

        this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

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
