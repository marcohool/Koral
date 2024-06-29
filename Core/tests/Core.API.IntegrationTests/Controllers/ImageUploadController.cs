using System.Net;
using FluentAssertions;

namespace Core.API.IntegrationTests.Controllers;

public class ImageUploadController(IntegrationTestWebApplicationFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task FavouriteImageUpload_FavouritesOwnUpload_ReturnsSuccessStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        // Act
        HttpResponseMessage response = await client.PutAsync("/imageupload/favourite/1", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task FavouriteImageUpload_FavouritesNonExistingUpload_ReturnsSuccessStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        // Act
        HttpResponseMessage response = await client.PutAsync("/imageupload/favourite/999", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task FavouriteImageUpload_FavouritesAnotherUserUpload_ReturnsSuccessStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        // Act
        HttpResponseMessage response = await client.PutAsync("/imageupload/favourite/2", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
