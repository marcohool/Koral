using Core.API.Dto.ClothingItem;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Services;

/// <summary>
/// Interface for the <see cref="IClothingItemService"/> class.
/// </summary>
public interface IClothingItemService
{
    /// <summary>
    /// Gets a list of all clothing items from the database.
    /// </summary>
    /// <returns>A list of <see cref="ClothingItemRequest"/> objects</returns>
    Task<IEnumerable<ClothingItemRequest>> GetClothingItemsAsync();

    /// <summary>
    /// Gets a clothing item from the database by its ID.
    /// </summary>
    /// <param name="id">Clothing item ID.</param>
    /// <returns>The created <see cref="ClothingItemRequest"/> item or null.</returns>

    Task<ClothingItemRequest?> GetClothingItemAsync(int id);

    /// <summary>
    /// Creates a new clothing item in the database.
    /// </summary>
    /// <param name="clothingItemDto">Instance of <see cref="ClothingItemResponse"/>.</param>
    /// <returns>The created clothing item.</returns>
    Task<ClothingItemResponse> CreateClothingItemAsync(ClothingItemRequest clothingItemDto);
}
