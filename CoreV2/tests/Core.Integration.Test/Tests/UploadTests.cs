using Core.Application.Dtos.Upload;
using Core.Integration.Test.Helpers;
using FluentAssertions;
using Newtonsoft.Json;

namespace Core.Integration.Test.Tests;

public class UploadTests(CustomWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllUserUploads()
    {
        HttpClient client = this.HttpClient;

        HttpResponseMessage response = await client.GetAsync("/uploads");

        response.EnsureSuccessStatusCode();

        List<UploadResponseDto>? uploads = JsonConvert.DeserializeObject<List<UploadResponseDto>>(
            await response.Content.ReadAsStringAsync()
        );

        uploads
            .Should()
            .HaveCount(
                this.DbContext.Uploads.Where(u =>
                        u.AppUserId == DatabaseContextHelper.authenticatedUser.Id
                    )
                    .Count()
            );
    }

    [Fact]
    public async Task GetAllAsync_WithPagination_ReturnsPagedUserUploads()
    {
        HttpClient client = this.HttpClient;

        HttpResponseMessage response = await client.GetAsync("/uploads?pageNumber=1&pageSize=3");

        response.EnsureSuccessStatusCode();

        List<UploadResponseDto>? uploads = JsonConvert.DeserializeObject<List<UploadResponseDto>>(
            await response.Content.ReadAsStringAsync()
        );

        uploads.Should().HaveCount(3);
    }
}
