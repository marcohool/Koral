using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Linq.Expressions;
using AutoMapper;
using Core.Application.MappingProfiles;
using Core.Application.Models;
using Core.Application.Models.Upload;
using Core.Application.Services;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Core.Test.Shared.Helpers;
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
            new() { Image = FileHelpers.CreateMockFormFile(10, "image-upload.jpg", "image/jpg") };

        this.claimServiceMock.Setup(x => x.GetCurrentUserAsync())
            .ThrowsAsync(new InvalidOperationException("User not found"));

        await this
            .uploadService.Invoking(x => x.CreateAsync(createUploadDto))
            .Should()
            .ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task CreateAsync_InvalidImage_ThrowsValidationException()
    {
        CreateUploadDto createUploadDto =
            new() { Image = FileHelpers.CreateMockFormFile(10, "invalid-upload.bmp", "image/bmp") };

        this.claimServiceMock.Setup(x => x.GetCurrentUserAsync()).ReturnsAsync(this.user);

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
            new() { Image = FileHelpers.CreateMockFormFile(10, "image-upload.jpg", "image/jpg") };

        Upload upload =
            new()
            {
                AppUserId = this.user.Id,
                ContentType = createUploadDto.Image.ContentType,
                Size = createUploadDto.Image.Length,
                ImageUrl = uploadUrl
            };

        this.claimServiceMock.Setup(x => x.GetCurrentUserAsync()).ReturnsAsync(this.user);

        this.imageStorageServiceMock.Setup(x => x.UploadImageAsync(createUploadDto.Image, default))
            .ReturnsAsync(uploadUrl);

        this.uploadRepositoryMock.Setup(x =>
                x.AddAsync(It.Is<Upload>(u => u.IsEquivalentJson(upload)))
            )
            .Returns(Task.CompletedTask);

        UploadDto result = await this.uploadService.CreateAsync(createUploadDto);

        result.ShouldBeEquivalentTo(
            new UploadDto()
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

    [Fact]
    public async Task GetAllAsync_WithValidData_ReturnsUploadResponseDtos()
    {
        this.claimServiceMock.Setup(x => x.GetCurrentUserAsync()).ReturnsAsync(this.user);

        List<Upload> uploads =
        [
            new Upload()
            {
                AppUserId = this.user.Id,
                ContentType = "image/jpg",
                Size = 10,
                ImageUrl = "https://www.example.com/image.jpg"
            },
            new Upload()
            {
                AppUserId = this.user.Id,
                ContentType = "image/png",
                Size = 20,
                ImageUrl = "https://www.example.com/image.png"
            }
        ];

        this.uploadRepositoryMock.Setup(x =>
                x.CountAsync(It.IsAny<Expression<Func<Upload, bool>>>())
            )
            .ReturnsAsync(uploads.Count);

        this.uploadRepositoryMock.Setup(x =>
                x.GetAllAsync(It.IsAny<Expression<Func<Upload, bool>>>(), 1, 5)
            )
            .ReturnsAsync(uploads);

        PaginatedResponse<UploadDto> result = await this.uploadService.GetAllAsync(1, 5);

        result.ShouldBeEquivalentTo(
            new PaginatedResponse<UploadDto>(
                data:
                [
                    new()
                    {
                        Status = Domain.Enums.UploadStatus.Pending,
                        ImageUrl = "https://www.example.com/image.jpg",
                        CreatedOn = DateTime.MinValue,
                        LastUpdatedOn = null
                    },
                    new()
                    {
                        Status = Domain.Enums.UploadStatus.Pending,
                        ImageUrl = "https://www.example.com/image.png",
                        CreatedOn = DateTime.MinValue,
                        LastUpdatedOn = null
                    }
                ],
                currentPage: 1,
                pageSize: 5,
                totalRecords: 2
            )
        );

        this.claimServiceMock.VerifyAll();
        this.uploadRepositoryMock.VerifyAll();
    }
}
