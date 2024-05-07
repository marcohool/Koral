using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.API.Models;
using FluentAssertions;
using Newtonsoft.Json;

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
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        List<ClothingItem>? clothingItems = JsonConvert.DeserializeObject<List<ClothingItem>>(
            await response.Content.ReadAsStringAsync()
        );

        clothingItems.Should().BeOfType<List<ClothingItem>>();
        clothingItems.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetClothingItem_ReturnsSuccessStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        // Act
        HttpResponseMessage response = await client.GetAsync("/clothingitem/1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        ClothingItem? clothingItem = JsonConvert.DeserializeObject<ClothingItem>(
            await response.Content.ReadAsStringAsync()
        );

        clothingItem.Should().BeOfType<ClothingItem>();
        clothingItem.Should().NotBeNull();
    }
}
