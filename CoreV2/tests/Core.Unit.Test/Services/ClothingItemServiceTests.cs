using AutoMapper;
using Core.Application.Services;
using Core.DataAccess.Repositories.Interfaces;
using Moq;

namespace Core.UnitTest.Services;

public class ClothingItemServiceTests
{
    private readonly Mock<IMapper> mapperMock;
    private readonly Mock<IClothingItemRepository> clothingItemRepositoryMock;

    private readonly ClothingItemService clothingItemService;

    public ClothingItemServiceTests()
    {
        this.mapperMock = new Mock<IMapper>(MockBehavior.Strict);
        this.clothingItemRepositoryMock = new Mock<IClothingItemRepository>(MockBehavior.Strict);

        this.clothingItemService = new ClothingItemService(
            this.mapperMock.Object,
            this.clothingItemRepositoryMock.Object
        );
    }
}
