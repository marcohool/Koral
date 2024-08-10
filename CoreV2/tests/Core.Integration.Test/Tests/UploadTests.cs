namespace Core.Integration.Test.Tests;

public class UploadTests(CustomWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllUploads()
    {
        HttpClient client = this.HttpClient;

        HttpResponseMessage response = await client.GetAsync("/uploads");

        response.EnsureSuccessStatusCode();
    }
}
