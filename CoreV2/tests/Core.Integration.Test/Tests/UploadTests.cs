﻿using System.Net;
using System.Net.Http.Headers;
using CloudinaryDotNet.Actions;
using Core.Application.Dtos.Upload;
using Core.Domain.Entities;
using Core.Integration.Test.Helpers;
using Core.Test.Shared.Helpers;
using Core.UnitTest.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
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

    [Fact]
    public async Task GetAsync_UploadFound_ReturnsUploadResponse()
    {
        HttpClient client = this.HttpClient;
        Upload upload = DatabaseContextHelper.Uploads[0];

        HttpResponseMessage response = await client.GetAsync($"/uploads/{upload.Id}");

        response.EnsureSuccessStatusCode();

        UploadResponseDto? uploadResponse = JsonConvert.DeserializeObject<UploadResponseDto>(
            await response.Content.ReadAsStringAsync()
        );

        uploadResponse.Should().NotBeNull();
        uploadResponse?.Id.Should().Be(upload.Id);
    }

    [Fact]
    public async Task GetAsync_UploadNotFound_Returns404NotFound()
    {
        HttpClient client = this.HttpClient;

        HttpResponseMessage response = await client.GetAsync($"/uploads/{Guid.NewGuid}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAsync_UploadFromAnotherUser_Returns404NotFound()
    {
        HttpClient client = this.HttpClient;
        Upload upload = DatabaseContextHelper.Uploads.First(u =>
            u.AppUserId != DatabaseContextHelper.authenticatedUser.Id
        );

        HttpResponseMessage response = await client.GetAsync($"/uploads/{upload.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateAsync_ValidUploadRequest_ReturnsUploadResponse()
    {
        HttpClient client = this.HttpClient;
        FormFile image = FileHelpers.CreateMockFormFile(10, "test.jpg", "image/jpg");

        this.MockCloudinaryService.Setup(x =>
                x.UploadAsync(
                    It.Is<ImageUploadParams>(up => up.File.FileName.Equals(image.FileName))
                )
            )
            .ReturnsAsync(
                new ImageUploadResult()
                {
                    SecureUrl = new Uri("https://www.example.com/image.jpg"),
                }
            );

        StreamContent imageContent = new(image.OpenReadStream());
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

        MultipartFormDataContent content =
            new() { { imageContent, nameof(CreateUploadDto.Image), "test.jpg" } };

        HttpResponseMessage response = await client.PostAsync("/uploads", content);

        response.EnsureSuccessStatusCode();

        UploadResponseDto? uploadResponse = JsonConvert.DeserializeObject<UploadResponseDto>(
            await response.Content.ReadAsStringAsync()
        );

        uploadResponse.Should().NotBeNull();
        uploadResponse?.ImageUrl.Should().BeEquivalentTo("https://www.example.com/image.jpg");

        this.MockCloudinaryService.VerifyAll();
    }

    [Fact]
    public async Task CreateAsync_InvalidImage_Returns400BadRequest()
    {
        HttpClient client = this.HttpClient;
        FormFile image = FileHelpers.CreateMockFormFile(10, "test.bmp", "image/bmp");

        StreamContent imageContent = new(image.OpenReadStream());
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/bmp");

        MultipartFormDataContent content =
            new() { { imageContent, nameof(CreateUploadDto.Image), "test.bmp" } };

        HttpResponseMessage response = await client.PostAsync("/uploads", content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteAsync_UploadFound_Returns204NoContent()
    {
        HttpClient client = this.HttpClient;
        Upload upload = DatabaseContextHelper.Uploads[0];

        this.MockCloudinaryService.Setup(x =>
                x.DeleteAsync(
                    It.Is<DeletionParams>(dp =>
                        dp.IsEquivalentJson(new DeletionParams("old-money-summer-look"))
                    )
                )
            )
            .ReturnsAsync(new DeletionResult() { Result = "ok" });

        HttpResponseMessage response = await client.DeleteAsync($"/uploads/{upload.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        this.MockCloudinaryService.VerifyAll();
    }

    [Fact]
    public async Task DeleteAsync_UploadNotFound_Returns404NotFound()
    {
        HttpClient client = this.HttpClient;

        HttpResponseMessage response = await client.DeleteAsync($"/uploads/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteAsync_UploadFromAnotherUser_Returns404NotFound()
    {
        HttpClient client = this.HttpClient;
        Upload upload = DatabaseContextHelper.Uploads.First(u =>
            u.AppUserId != DatabaseContextHelper.authenticatedUser.Id
        );

        HttpResponseMessage response = await client.DeleteAsync($"/uploads/{upload.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}