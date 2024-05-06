using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.IntegrationTests.Controllers;

public class ClothingItemTests(IntegrationTestWebApplicationFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetClothingItems_ReturnsSuccessStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        // Act
        HttpResponseMessage response = await client.GetAsync("/clothingitem");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
