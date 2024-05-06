using Core.API.Dto;
using Core.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Services;

/// <summary>
/// The <see cref="ClothingItemService"/> class.
/// </summary>
public class ClothingItemService(IClothingItemRepository clothingItemRepository)
    : IClothingItemService
{
    private readonly IClothingItemRepository clothingItemRepository = clothingItemRepository;

    /// <summary>
    /// Gets a list of all clothing items from the database.
    /// </summary>
    /// <returns>A list of <see cref="ClothingItemDto"/> objects</returns>
    public async Task<IEnumerable<ClothingItemDto>> GetClothingItemsAsync()
    {
        try
        {
            return await this.clothingItemRepository.GetClothingItemsAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
