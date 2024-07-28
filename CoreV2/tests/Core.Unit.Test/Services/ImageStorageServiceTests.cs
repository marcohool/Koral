using CloudinaryDotNet;
using Core.Application.Configuration;
using Core.Application.Services;
using Microsoft.Extensions.Options;
using Moq;
using CloudinaryConfiguration = Core.Application.Configuration.CloudinaryConfiguration;

namespace Core.UnitTest.Services;

public class ImageStorageServiceTests
{
    private readonly Mock<Cloudinary> cloudinaryMock;
    private Mock<IOptionsMonitor<CloudinaryConfiguration>> mockCloudinaryConfig;
    private Mock<IOptionsMonitor<ImageOptions>> mockImageOptions;

    private readonly ImageStorageService imageStorageService;

    public ImageStorageServiceTests()
    {
        this.mockCloudinaryConfig = new Mock<IOptionsMonitor<CloudinaryConfiguration>>(
            MockBehavior.Strict
        );
        this.mockImageOptions = new Mock<IOptionsMonitor<ImageOptions>>(MockBehavior.Strict);
        this.cloudinaryMock = new Mock<Cloudinary>(MockBehavior.Strict);
    }

    [Fact]
    public async Task UploadImageAsync_ValidImage_ReturnsSecureUrl() { }
}
