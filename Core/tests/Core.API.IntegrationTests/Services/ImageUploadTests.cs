using System.Net;
using System.Net.Http.Headers;
using FluentAssertions;

namespace Core.API.IntegrationTests.Services;

public class ImageUploadTests(IntegrationTestWebApplicationFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task UploadImage_ReturnsSuccessStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        FileStream fileStream = File.OpenRead(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "../../../StaticFiles/Images/sample-10mb.jpg"
            )
        );
        StreamContent fileContent = new(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

        MultipartFormDataContent content =
            new() { { fileContent, "ImageFile", "sample-10mb.jpg" } };

        // Act
        HttpResponseMessage response = await client.PostAsync("/imageupload", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
