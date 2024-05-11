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

    [Fact]
    public async Task PostClothingItem_ReturnsCreatedStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        ClothingItem clothingItem =
            new()
            {
                Name = "Test Clothing Item",
                Description = "Test Description",
                Brand = "Test Brand",
                Category = "Test Category",
                Colour = "Test Colour",
                Price = 9.99m,
                ImageURL = "https://test.com/image.jpg",
                SourceURL = "https://test.com",
                LastChecked = DateTime.Now
            };

        string json = JsonConvert.SerializeObject(clothingItem);
        StringContent content = new(json, Encoding.UTF8, "application/json");

        // Act
        HttpResponseMessage response = await client.PostAsync("/clothingitem", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        ClothingItem? createdClothingItem = JsonConvert.DeserializeObject<ClothingItem>(
            await response.Content.ReadAsStringAsync()
        );

        createdClothingItem.Should().BeOfType<ClothingItem>();
        createdClothingItem.Should().NotBeNull();
    }

    [Fact]
    public async Task PutClothingItem_UpdatesClothingItem_ReturnsSuccessStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        ClothingItem clothingItem =
            new()
            {
                Id = 1,
                Name = "Test Updated Clothing Item",
                Description = "Test Updated Description",
                Brand = "Test Updated Brand",
                Category = "Test Updated Category",
                Colour = "Test Updated Colour",
                Price = 20.99m,
                ImageURL = "https://test.com/updated-image.jpg",
                SourceURL = "https://test.com/updated",
                LastChecked = DateTime.Now
            };

        string json = JsonConvert.SerializeObject(clothingItem);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        HttpResponseMessage response = await client.PutAsync("/clothingitem/1", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        ClothingItem? updatedClothingItem = JsonConvert.DeserializeObject<ClothingItem>(
            await response.Content.ReadAsStringAsync()
        );

        updatedClothingItem.Should().BeOfType<ClothingItem>();
        updatedClothingItem.Should().NotBeNull();
    }

    [Fact]
    public async Task PutClothingItem_NonexistentClothingItem_ReturnsNotFoundStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        ClothingItem clothingItem =
            new()
            {
                Id = 999,
                Name = "Test Updated Clothing Item",
                Description = "Test Updated Description",
                Brand = "Test Updated Brand",
                Category = "Test Updated Category",
                Colour = "Test Updated Colour",
                Price = 20.99m,
                ImageURL = "https://test.com/updated-image.jpg",
                SourceURL = "https://test.com/updated",
                LastChecked = DateTime.Now
            };

        string json = JsonConvert.SerializeObject(clothingItem);
        StringContent content = new(json, Encoding.UTF8, "application/json");

        // Act
        HttpResponseMessage response = await client.PutAsync("/clothingitem/999", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteClothingItem_ReturnsNoContentCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        // Act
        HttpResponseMessage response = await client.DeleteAsync("/clothingitem/1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteClothingItem_NonexistentClothingItem_ReturnsNotFoundStatusCode()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        // Act
        HttpResponseMessage response = await client.DeleteAsync("/clothingitem/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
