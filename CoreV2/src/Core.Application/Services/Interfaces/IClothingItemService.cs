using Core.Application.Dtos.ClothingItem;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services.Interfaces;

public interface IClothingItemService
{
    Task<ClothingItemResponseDto> CreateAsync(
        CreateClothingItemDto createClothingItemModel,
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

    Task ImportClothingItems(IFormFile file);
}
