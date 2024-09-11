using Core.Application.Dtos.ClothingItem;

namespace Core.Application.Services.Interfaces;

public interface IClothingItemService
{
    Task<ClothingItemResponseDto> CreateAsync(
        CreateClothingItemDto createClothingItemRequestModel,
        CancellationToken cancellationToken = default
    );

    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<ClothingItemResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    );

    Task<ClothingItemResponseDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    Task UpsertCollectionAsync(
        IEnumerable<ClothingItemImport> clothingItemImports,
        CancellationToken cancellationToken = default
    );
}
