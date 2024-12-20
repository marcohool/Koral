using AutoMapper;
using Core.Application.Exceptions;
using Core.Application.MappingProfiles;
using Core.Application.Models.ClothingItem;
using Core.Application.Services;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Test.Shared.Helpers;
using Core.UnitTest.Shared;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Core.UnitTest.Services;

public class ClothingItemServiceTests : BaseServiceTests
{
    private readonly Mock<IClothingItemRepository> clothingItemRepositoryMock;
    private readonly Mock<IImageStorageService> imageStorageServiceMock;

    private readonly IMapper mapper;
    private readonly ClothingItemService clothingItemService;

    public ClothingItemServiceTests()
    {
        this.imageStorageServiceMock = new Mock<IImageStorageService>(MockBehavior.Strict);
        this.clothingItemRepositoryMock = new Mock<IClothingItemRepository>(MockBehavior.Strict);

        this.mapper = new MapperConfiguration(cfg =>
            cfg.AddMaps(typeof(ClothingItemProfile))
        ).CreateMapper();

        this.clothingItemService = new ClothingItemService(
            this.mapper,
            this.clothingItemRepositoryMock.Object,
            this.imageStorageServiceMock.Object
        );
    }

    [Fact]
    public async Task CreateAsync_ValidClothingItem_ReturnsCreatedClothingItem()
    {
        CreateClothingItemDto createClothingItemDto =
            new()
            {
                Name = "White T-Shirt",
                Description = "A plain white t-shirt",
                Brand = "Zara",
                Category = "T-Shirts",
                Colour = "White",
                Price = (decimal)10.99,
                CurrencyCode = "GBP",
                Gender = Gender.Male,
                SourceUrl = "https://example.com/white-t-shirt",
                SourceRegion = SourceRegion.UK,
                Image = FileHelpers.CreateMockFormFile(10, "white-tshirt.jpg", "image/jpeg"),
            };

        ClothingItem clothingItem = this.mapper.Map<ClothingItem>(createClothingItemDto);
        clothingItem.ImageUrl = "https://example-image-hosting.com/white-tshirt.jpg";

        this.imageStorageServiceMock.Setup(i =>
                i.UploadImageAsync(createClothingItemDto.Image, default)
            )
            .ReturnsAsync("https://example-image-hosting.com/white-tshirt.jpg");

        this.clothingItemRepositoryMock.Setup(c =>
                c.AddAsync(It.Is<ClothingItem>(ci => ci.IsEquivalentJson(clothingItem)))
            )
            .Returns(Task.CompletedTask);

        ClothingItemResponseDto result = await this.clothingItemService.CreateAsync(
            createClothingItemDto
        );

        result
            .Should()
            .BeEquivalentTo(
                new ClothingItemResponseDto()
                {
                    Name = "White T-Shirt",
                    Description = "A plain white t-shirt",
                    Brand = "Zara",
                    Category = "T-Shirts",
                    Colour = "White",
                    Price = (decimal)10.99,
                    CurrencyCode = "GBP",
                    Gender = Gender.Male,
                    SourceUrl = "https://example.com/white-t-shirt",
                    SourceRegion = SourceRegion.UK,
                    ImageUrl = "https://example-image-hosting.com/white-tshirt.jpg"
                }
            );

        this.imageStorageServiceMock.VerifyAll();
        this.clothingItemRepositoryMock.VerifyAll();
    }

    [Fact]
    public async Task CreateAsync_InvalidImage_ThrowsValidationException()
    {
        CreateClothingItemDto createClothingItemDto =
            new()
            {
                Name = "White T-Shirt",
                Description = "A plain white t-shirt",
                Brand = "Zara",
                Category = "T-Shirts",
                Colour = "White",
                Price = (decimal)10.99,
                CurrencyCode = "GBP",
                Gender = Gender.Male,
                SourceUrl = "https://example.com/white-t-shirt",
                SourceRegion = SourceRegion.UK,
                Image = FileHelpers.CreateMockFormFile(11, "white-tshirt.jpg", "image/jpeg"),
            };

        this.imageStorageServiceMock.Setup(i =>
                i.UploadImageAsync(createClothingItemDto.Image, default)
            )
            .ThrowsAsync(new ValidationException());

        await this
            .clothingItemService.Invoking(x => x.CreateAsync(createClothingItemDto))
            .Should()
            .ThrowAsync<ValidationException>();

        this.imageStorageServiceMock.VerifyAll();
    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnsDeletedClothingItemId()
    {
        Guid clothingItemId = Guid.NewGuid();
        Mock<IDbContextTransaction> mockTransaction = new();

        this.clothingItemRepositoryMock.Setup(c => c.BeginTransactionAsync())
            .ReturnsAsync(mockTransaction.Object);

        this.clothingItemRepositoryMock.Setup(c => c.GetFirstAsync(ci => ci.Id == clothingItemId))
            .ReturnsAsync(
                new ClothingItem
                {
                    Id = clothingItemId,
                    Name = "White T-Shirt",
                    Gender = Gender.Male,
                    SourceUrl = "www.example.com/white-t-shirt",
                    SourceRegion = SourceRegion.UK,
                }
            );

        this.clothingItemRepositoryMock.Setup(c =>
                c.DeleteAsync(It.Is<ClothingItem>(ci => ci.Id == clothingItemId))
            )
            .ReturnsAsync(clothingItemId);

        Guid result = await this.clothingItemService.DeleteAsync(clothingItemId);

        result.Should().Be(clothingItemId);

        this.clothingItemRepositoryMock.VerifyAll();
    }

    [Fact]
    public async Task DeleteAsync_NonExistentEntity_ReturnsNotFoundException()
    {
        Guid clothingItemId = Guid.NewGuid();

        this.clothingItemRepositoryMock.Setup(c => c.GetFirstAsync(ci => ci.Id == clothingItemId))
            .ThrowsAsync(
                new NotFoundException($"Clothing item with id {clothingItemId} not found")
            );

        await this
            .clothingItemService.Invoking(x => x.DeleteAsync(clothingItemId))
            .Should()
            .ThrowAsync<NotFoundException>();

        this.clothingItemRepositoryMock.VerifyAll();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllClothingItems()
    {
        List<ClothingItem> clothingItems =
        [
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "White T-Shirt",
                Gender = Gender.Male,
                SourceUrl = "www.example.com/white-t-shirt",
                SourceRegion = SourceRegion.UK,
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Black T-Shirt",
                Gender = Gender.Unisex,
                SourceUrl = "www.example.com/black-t-shirt",
                SourceRegion = SourceRegion.US
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Blue T-Shirt",
                Gender = Gender.Female,
                SourceUrl = "www.example.com/blue-t-shirt",
                SourceRegion = SourceRegion.DE
            }
        ];

        this.clothingItemRepositoryMock.Setup(c => c.GetAllAsync(null, null, default))
            .ReturnsAsync(clothingItems);

        IEnumerable<ClothingItemResponseDto> result = await this.clothingItemService.GetAllAsync();

        result
            .Should()
            .BeEquivalentTo(
                clothingItems.Select(ci => this.mapper.Map<ClothingItemResponseDto>(ci))
            );

        this.clothingItemRepositoryMock.VerifyAll();
    }

    [Fact]
    public async Task GetAllAsync_NoItems_ReturnsEmptyEnumerable()
    {
        this.clothingItemRepositoryMock.Setup(c => c.GetAllAsync(null, null, default))
            .ReturnsAsync([]);

        IEnumerable<ClothingItemResponseDto> result = await this.clothingItemService.GetAllAsync();

        result.Should().BeEquivalentTo<ClothingItemResponseDto>([]);

        this.clothingItemRepositoryMock.VerifyAll();
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsClothingItem()
    {
        Guid clothingItemId = Guid.NewGuid();

        ClothingItem clothingItem =
            new()
            {
                Id = clothingItemId,
                Name = "White T-Shirt",
                Gender = Gender.Male,
                SourceUrl = "www.example.com/white-t-shirt",
                SourceRegion = SourceRegion.UK,
            };

        this.clothingItemRepositoryMock.Setup(c => c.GetFirstAsync(ci => ci.Id == clothingItemId))
            .ReturnsAsync(clothingItem);

        ClothingItemResponseDto result = await this.clothingItemService.GetByIdAsync(
            clothingItemId
        );

        result.Should().BeEquivalentTo(this.mapper.Map<ClothingItemResponseDto>(clothingItem));

        this.clothingItemRepositoryMock.VerifyAll();
    }

    [Fact]
    public async Task GetByIdAsync_NonExistentEntity_ReturnsNotFoundException()
    {
        Guid clothingItemId = Guid.NewGuid();

        this.clothingItemRepositoryMock.Setup(c => c.GetFirstAsync(ci => ci.Id == clothingItemId))
            .ReturnsAsync((ClothingItem?)null);

        await this
            .clothingItemService.Invoking(x => x.GetByIdAsync(clothingItemId))
            .Should()
            .ThrowAsync<NotFoundException>();

        this.clothingItemRepositoryMock.VerifyAll();
    }
}
