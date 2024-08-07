using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Core.Application.Dtos.Upload;
using Core.Application.MappingProfiles;
using Core.Application.Services;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Identity;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Core.UnitTest.Shared;
using FluentAssertions;
using Moq;

namespace Core.UnitTest.Services;

public class UploadServiceTests : BaseServiceTests
{
    private readonly Mock<IImageStorageService> imageStorageServiceMock;
    private readonly Mock<IUploadRepository> uploadRepositoryMock;
    private readonly Mock<IClaimService> claimServiceMock;

    private readonly IMapper mapper;
    private readonly UploadService uploadService;

    public UploadServiceTests()
    {
        this.imageStorageServiceMock = new Mock<IImageStorageService>(MockBehavior.Strict);
        this.uploadRepositoryMock = new Mock<IUploadRepository>(MockBehavior.Strict);
        this.claimServiceMock = new Mock<IClaimService>(MockBehavior.Strict);

        this.mapper = new MapperConfiguration(cfg =>
            cfg.AddMaps(typeof(UploadProfile))
        ).CreateMapper();

        this.uploadService = new UploadService(
            this.mapper,
            this.imageStorageServiceMock.Object,
            this.uploadRepositoryMock.Object,
            this.claimServiceMock.Object
        );
    }

    [Fact]
    public async Task CreateAsync_UserNotFound_ThrowsInvalidOperationException()
    {
        CreateUploadDto createUploadDto =
            new() { Image = CreateMockFormFile(10, "image-upload.jpg", "image/jpg") };

        this.claimServiceMock.Setup(x => x.GetCurrentUserAsync())
            .ReturnsAsync((ApplicationUser?)null);

        await this
            .uploadService.Invoking(x => x.CreateAsync(createUploadDto))
            .Should()
            .ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task CreateAsync_InvalidImage_ThrowsValidationException()
    {
        CreateUploadDto createUploadDto =
            new() { Image = CreateMockFormFile(10, "invalid-upload.bmp", "image/bmp") };

        ApplicationUser user = new() { Id = Guid.NewGuid().ToString(), };

        this.claimServiceMock.Setup(x => x.GetCurrentUserAsync()).ReturnsAsync(user);

        this.imageStorageServiceMock.Setup(x => x.UploadImageAsync(createUploadDto.Image, default))
            .ThrowsAsync(new ValidationException("Invalid image format"));

        await this
            .uploadService.Invoking(x => x.CreateAsync(createUploadDto))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateAsync_WithValidData_ReturnsUploadResponseDto()
    {
        string uploadUrl = "https://www.example.com/image.jpg";

        CreateUploadDto createUploadDto =
            new() { Image = CreateMockFormFile(10, "image-upload.jpg", "image/jpg") };

        ApplicationUser user = new() { Id = Guid.NewGuid().ToString(), };

        Upload upload =
            new()
            {
                AppUserId = user.Id,
                ContentType = createUploadDto.Image.ContentType,
                Size = createUploadDto.Image.Length,
                ImageUrl = uploadUrl
            };

        this.claimServiceMock.Setup(x => x.GetCurrentUserAsync()).ReturnsAsync(user);

        this.imageStorageServiceMock.Setup(x => x.UploadImageAsync(createUploadDto.Image, default))
            .ReturnsAsync(uploadUrl);

        this.uploadRepositoryMock.Setup(x =>
                x.AddAsync(It.Is<Upload>(u => u.IsEquivalentJson(upload)))
            )
            .ReturnsAsync(upload);

        UploadResponseDto result = await this.uploadService.CreateAsync(createUploadDto);

        result.ShouldBeEquivalentTo(
            new UploadResponseDto()
            {
                Status = Domain.Enums.UploadStatus.Pending,
                ImageUrl = uploadUrl,
                CreatedOn = DateTime.MinValue,
                LastUpdatedOn = null
            }
        );

        this.claimServiceMock.VerifyAll();
        this.imageStorageServiceMock.VerifyAll();
        this.uploadRepositoryMock.VerifyAll();
    }
}
