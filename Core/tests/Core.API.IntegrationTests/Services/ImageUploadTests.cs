using System.Net;
using System.Net.Http.Headers;
using Core.API.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace Core.API.IntegrationTests.Services;

public class ImageUploadTests(IntegrationTestWebApplicationFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetImageUploads_ReturnsListOfImageUploads()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        // Act
        HttpResponseMessage response = await client.GetAsync("/imageupload?pageNumber=0");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        List<ImageUpload>? imageUploads = JsonConvert.DeserializeObject<List<ImageUpload>>(
            await response.Content.ReadAsStringAsync()
        );

        imageUploads.Should().BeOfType<List<ImageUpload>>();
        imageUploads.Should().HaveCount(1);
    }

    [Fact]
    public async Task UploadImage_ReturnsCreatedStatusCode()
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
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
