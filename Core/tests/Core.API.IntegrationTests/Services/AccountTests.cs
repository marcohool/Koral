using System.Net;
using System.Net.Http.Json;
using System.Text;
using Core.API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;

namespace Core.API.IntegrationTests.Services;

public class AccountTests(IntegrationTestWebApplicationFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Register_ReturnsSuccessStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        RegisterRequest registerRequest =
            new() { Email = "valid@email.com", Password = "validpassword1" };

        string json = JsonConvert.SerializeObject(registerRequest);
        StringContent content = new(json, Encoding.UTF8, "application/json");

        // Act
        HttpResponseMessage response = await client.PostAsync("/account/register", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
