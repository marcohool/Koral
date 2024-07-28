using AutoMapper;
using Core.Application.Dtos.ClothingItem;
using Core.Application.MappingProfiles;
using Core.Application.Services;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Core.UnitTest.Services;

public class ClothingItemServiceTests
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
                Image = CreateMockFormFile(10, "white-tshirt.jpg", "image/jpeg"),
            };

        ClothingItem clothingItem = this.mapper.Map<ClothingItem>(createClothingItemDto);
        clothingItem.ImageUrl = "https://example-image-hosting.com/white-tshirt.jpg";

        this.imageStorageServiceMock.Setup(i =>
                i.UploadImageAsync(createClothingItemDto.Image, default)
            )
            .ReturnsAsync("https://example-image-hosting.com/white-tshirt.jpg");

        this.clothingItemRepositoryMock.Setup(c => c.AddAsync(It.IsAny<ClothingItem>()))
            .ReturnsAsync(clothingItem);

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
    }

    private static FormFile CreateMockFormFile(int fileSize, string fileName, string contentType)
    {
        byte[] fileContext = new byte[fileSize * 1024 * 1024];
        MemoryStream memoryStream = new MemoryStream(fileContext);

        return new FormFile(memoryStream, 0, fileSize, "file", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };
    }
}
