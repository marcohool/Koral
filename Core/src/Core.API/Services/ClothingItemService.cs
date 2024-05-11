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
        try
        {
            List<ClothingItem> clothingItems =
                await this.clothingItemRepository.GetClothingItemsAsync();

            return clothingItems.Select(item => new ClothingItemResponse(item)).ToList();
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
    public async Task<ClothingItemResponse?> GetClothingItemAsync(int id)
    {
        ClothingItem? clothingItem;
        try
        {
            clothingItem = await this.clothingItemRepository.GetClothingItemAsync(id);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                "An error occurred while retrieving the clothing item.",
                e
            );
        }

        return clothingItem != null ? new ClothingItemResponse(clothingItem) : null;
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

    /// <inheritdoc />
    public async Task<ClothingItemResponse> UpdateClothingItemAsync(
        int id,
        ClothingItemRequest clothingItemDto
    )
    {
        ClothingItem? clothingItem =
            await this.clothingItemRepository.GetClothingItemAsync(id)
            ?? throw new KeyNotFoundException("The clothing item does not exist.");

        try
        {
            return new ClothingItemResponse(
                await this.clothingItemRepository.UpdateClothingItemAsync(clothingItem)
            );
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                "An error occurred while updating the clothing item.",
                e
            );
        }
    }

    /// <inheritdoc />
    public async Task DeleteClothingItem(int id)
    {
        ClothingItem? clothingItem =
            await this.clothingItemRepository.GetClothingItemAsync(id)
            ?? throw new KeyNotFoundException("The clothing item does not exist.");

        try
        {
            await this.clothingItemRepository.DeleteClothingItem(clothingItem);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                "An error occurred while deleting the clothing item.",
                e
            );
        }
    }
}
