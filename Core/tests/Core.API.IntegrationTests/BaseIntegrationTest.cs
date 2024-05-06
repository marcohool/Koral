using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.API.IntegrationTests;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebApplicationFactory>
{
    protected BaseIntegrationTest(IntegrationTestWebApplicationFactory factory)
    {
        this.HttpClient = factory.CreateClient();
    }

    protected HttpClient HttpClient { get; }
}
