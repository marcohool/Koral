using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet.Actions;
using Core.Application.Configuration;
using Core.Application.Services;
using Core.Application.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;

namespace Core.UnitTest.Services;

public class ImageStorageServiceTests : BaseServiceTests
{
    private readonly Mock<ICloudinaryService> mockCloudinaryService;
    private readonly Mock<IOptionsMonitor<ImageOptions>> mockImageOptions;

    private readonly ImageStorageService imageStorageService;

    public ImageStorageServiceTests()
    {
        this.mockImageOptions = new Mock<IOptionsMonitor<ImageOptions>>(MockBehavior.Strict);
        this.mockCloudinaryService = new Mock<ICloudinaryService>(MockBehavior.Strict);

        this.imageStorageService = new ImageStorageService(
            this.mockCloudinaryService.Object,
            this.mockImageOptions.Object
        );
    }

    [Theory]
    [InlineData("image.jpg", "image/jpeg")]
    [InlineData("image.jpeg", "image/jpeg")]
    [InlineData("image.png", "image/png")]
    public async Task UploadImageAsync_ValidImage_ReturnsSecureUrl(
        string fileName,
        string contentType
    )
    {
        string expectedUploadUrl = "https://example-image-hosting.com/image.jpg";
        FormFile validImage = CreateMockFormFile(10, fileName, contentType);

        this.mockImageOptions.SetupGet(i => i.CurrentValue)
            .Returns(
                new ImageOptions
                {
                    AllowedExtensions = [".jpg", ".jpeg", ".png"],
                    MaxSize = 10 * 1024 * 1024
                }
            );

        this.mockCloudinaryService.Setup(c => c.UploadAsync(It.IsAny<ImageUploadParams>()))
            .ReturnsAsync(new ImageUploadResult { SecureUrl = new Uri(expectedUploadUrl) });

        string uploadUrl = await this.imageStorageService.UploadImageAsync(validImage);

        uploadUrl.Should().Be(expectedUploadUrl);

        this.mockCloudinaryService.VerifyAll();
        this.mockImageOptions.VerifyAll();
    }

    [Fact]
    public async Task UploadImageAsync_InvalidImage_ExceedsMaxSize_ThrowsValidationError()
    {
        FormFile validImage = CreateMockFormFile(11, "image.jpg", "image/jpeg");

        this.mockImageOptions.SetupGet(i => i.CurrentValue)
            .Returns(
                new ImageOptions
                {
                    AllowedExtensions = [".jpg", ".jpeg", ".png"],
                    MaxSize = 10 * 1024 * 1024
                }
            );

        await Assert.ThrowsAsync<ValidationException>(
            async () => await this.imageStorageService.UploadImageAsync(validImage)
        );

        this.mockCloudinaryService.VerifyAll();
        this.mockImageOptions.VerifyAll();
    }

    [Theory]
    [InlineData("image.gif", "image/gif")]
    [InlineData("image.bmp", "image/bmp")]
    public async Task UploadImageAsync_InvalidImage_ExtensionNotAllowed_ReturnsSecureUrl(
        string fileName,
        string contentType
    )
    {
        FormFile validImage = CreateMockFormFile(11, fileName, contentType);

        this.mockImageOptions.SetupGet(i => i.CurrentValue)
            .Returns(
                new ImageOptions
                {
                    AllowedExtensions = [".jpg", ".jpeg", ".png"],
                    MaxSize = 10 * 1024 * 1024
                }
            );

        await Assert.ThrowsAsync<ValidationException>(
            async () => await this.imageStorageService.UploadImageAsync(validImage)
        );

        this.mockCloudinaryService.VerifyAll();
        this.mockImageOptions.VerifyAll();
    }
}
