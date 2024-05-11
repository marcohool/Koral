using Core.API.Dto.ClothingItem;
using Core.API.Repository;

namespace Core.API.Services;

/// <summary>
/// The <see cref="ClothingItemService"/> class.
/// </summary>
/// <param name="clothingItemRepository">Instance of <see cref="IClothingItemRepository"/>.</param>
public class ClothingItemService(IClothingItemRepository clothingItemRepository)
    : IClothingItemService
{
    private readonly IClothingItemRepository clothingItemRepository = clothingItemRepository;

    /// <inheritdoc />
    public async Task<IEnumerable<ClothingItemRequest>> GetClothingItemsAsync()
    {
        try
        {
            return await this.clothingItemRepository.GetClothingItemsAsync();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                "An error occurred while retrieving clothing items.",
                e
            );
        }
    }

    /// <inheritdoc />
    public async Task<ClothingItemRequest?> GetClothingItemAsync(int id)
    {
        try
        {
            return await this.clothingItemRepository.GetClothingItemAsync(id);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                "An error occurred while retrieving the clothing item.",
                e
            );
        }
    }

    /// <inheritdoc />
    public async Task<ClothingItemResponse> CreateClothingItemAsync(
        ClothingItemRequest clothingItemDto
    )
    {
        try
        {
            return new ClothingItemResponse(
                await this.clothingItemRepository.CreateClothingItemAsync(
                    clothingItemDto.ToClothingItemModel()
                )
            );
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                "An error occurred while creating the clothing item.",
                e
            );
        }
    }
}
