using System.Net;
using System.Net.Http.Headers;
using Core.Application.Dtos.ClothingItem;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Test.Shared.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;

namespace Core.Integration.Test.Tests;

public class ClothingItemTests(CustomWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllClothingItems()
    {
        HttpClient client = this.HttpClient;

        HttpResponseMessage response = await client.GetAsync("/clothingitems");

        response.EnsureSuccessStatusCode();

        List<ClothingItem>? clothingItems = JsonConvert.DeserializeObject<List<ClothingItem>>(
            await response.Content.ReadAsStringAsync()
        );

        clothingItems.Should().HaveCount(this.DbContext.ClothingItems.Count());
    }

    [Fact]
    public async Task GetAsync_ReturnsClothingItem()
    {
        HttpClient client = this.HttpClient;

        ClothingItem clothingItem = this.DbContext.ClothingItems.First();

        HttpResponseMessage response = await client.GetAsync($"/clothingitems/{clothingItem.Id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetAsync_ClothingItemNotFound_Returns404NotFound()
    {
        HttpClient client = this.HttpClient;

        HttpResponseMessage response = await client.GetAsync($"/clothingitems/1000");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        ClothingItem? clothingItemResponse = JsonConvert.DeserializeObject<ClothingItem>(
            await response.Content.ReadAsStringAsync()
        );

        clothingItemResponse.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_ValidClothingItem_ReturnsClothingItem()
    {
        // Arrange
        HttpClient client = this.HttpClient;

        FormFile image = FileHelpers.CreateMockFormFile(10, "image-upload.jpg", "image/jpg");

        MultipartFormDataContent multipartContent =
            new()
            {
                { new StringContent("White T-Shirt"), nameof(CreateClothingItemDto.Name) },
                { new StringContent(Gender.Male.ToString()), nameof(CreateClothingItemDto.Gender) },
                {
                    new StringContent("https://example.com/white-t-shirt"),
                    nameof(CreateClothingItemDto.SourceUrl)
                }
            };

        StreamContent imageContent = new(image.OpenReadStream());
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
        multipartContent.Add(imageContent, nameof(CreateClothingItemDto.Image), image.FileName);

        this.MockImageStorageService.Setup(x =>
                x.UploadImageAsync(
                    It.Is<FormFile>(f => f.FileName.Equals("image-upload.jpg")),
                    default
                )
            )
            .ReturnsAsync("https://www.example.com/image.jpg");

        // Act
        HttpResponseMessage response = await client.PostAsync("/clothingitems", multipartContent);

        // Assert
        response.EnsureSuccessStatusCode();

        ClothingItem? clothingItemResponse = JsonConvert.DeserializeObject<ClothingItem>(
            await response.Content.ReadAsStringAsync()
        );

        clothingItemResponse.Should().NotBeNull();
        clothingItemResponse?.ImageUrl.Should().BeEquivalentTo("https://www.example.com/image.jpg");

        this.MockImageStorageService.VerifyAll();
    }
}
