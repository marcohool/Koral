using Core.API.Dto;
using Core.API.Repository;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IEnumerable<ClothingItemDto>> GetClothingItemsAsync()
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
    public async Task<ClothingItemDto?> GetClothingItemAsync(int id)
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
}
