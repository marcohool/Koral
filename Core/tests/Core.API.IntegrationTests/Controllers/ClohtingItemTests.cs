using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.IntegrationTests.Controllers;

public class ClohtingItemTests : BaseIntegrationTest
{
    public ClohtingItemTests(CustomWebApplicationFactory factory)
        : base(factory) { }

    [Fact]
    public async Task GetClothingItems_ReturnsSuccessStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        // Act
        HttpResponseMessage response = await client.GetAsync("/clothingitems");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}
