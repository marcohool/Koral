using Core.API.Dto.ClothingItem;
using Core.API.Models;
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
    public async Task<IEnumerable<ClothingItemResponse>> GetClothingItemsAsync()
    {
        List<ClothingItem> clothingItems =
            await this.clothingItemRepository.GetClothingItemsAsync();

        return clothingItems.Select(item => new ClothingItemResponse(item)).ToList();
    }

    /// <inheritdoc />
    public async Task<ClothingItemResponse?> GetClothingItemAsync(int id)
    {
        ClothingItem? clothingItem = await this.clothingItemRepository.GetClothingItemAsync(id);

        return clothingItem != null ? new ClothingItemResponse(clothingItem) : null;
    }

    /// <inheritdoc />
    public async Task<ClothingItemResponse> CreateClothingItemAsync(
        ClothingItemRequest clothingItemDto
    )
    {
        return new ClothingItemResponse(
            await this.clothingItemRepository.CreateClothingItemAsync(
                clothingItemDto.ToClothingItemModel()
            )
        );
    }

    /// <inheritdoc />
    public async Task<ClothingItemResponse> UpdateClothingItemAsync(
        int id,
        ClothingItemRequest clothingItemDto
    )
    {
        ClothingItem? clothingItem =
            await this.clothingItemRepository.GetClothingItemAsync(id)
            ?? throw new KeyNotFoundException();

        return new ClothingItemResponse(
            await this.clothingItemRepository.UpdateClothingItemAsync(clothingItem)
        );
    }

    /// <inheritdoc />
    public async Task DeleteClothingItem(int id)
    {
        ClothingItem? clothingItem =
            await this.clothingItemRepository.GetClothingItemAsync(id)
            ?? throw new KeyNotFoundException();

        await this.clothingItemRepository.DeleteClothingItem(clothingItem);
    }
}
