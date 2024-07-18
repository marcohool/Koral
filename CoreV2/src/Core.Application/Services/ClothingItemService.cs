using Core.Application.Dtos.ClothingItem;
using Core.Application.Services.Interfaces;

namespace Core.Application.Services;

public class ClothingItemService : IClothingItemService
{
    public Task<ClothingItemResponseDto> CreateAsync(
        CreateClothingItemDto createClothingItemRequestModel,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }

    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClothingItemResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }

    public Task<ClothingItemResponseDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }
}
