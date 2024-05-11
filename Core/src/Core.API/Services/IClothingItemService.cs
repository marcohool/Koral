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
    /// <returns>A list of <see cref="ClothingItemResponse"/> objects.</returns>
    Task<IEnumerable<ClothingItemResponse>> GetClothingItemsAsync();

    /// <summary>
    /// Gets a clothing item from the database by its ID.
    /// </summary>
    /// <param name="id">Clothing item ID.</param>
    /// <returns>A <see cref="ClothingItemResponse"/> representing the clothing item or null.</returns>

    Task<ClothingItemResponse?> GetClothingItemAsync(int id);

    /// <summary>
    /// Creates a new clothing item in the database.
    /// </summary>
    /// <param name="clothingItemDto">Instance of <see cref="ClothingItemResponse"/>.</param>
    /// <returns>A <see cref="ClothingItemResponse"/> representing the created clothing item.</returns>
    Task<ClothingItemResponse> CreateClothingItemAsync(ClothingItemRequest clothingItemDto);

    /// <summary>
    /// Updates a clothing item in the database.
    /// </summary>
    /// <param name="id">The id of the clothing item</param>
    /// <param name="clothingItemDto">The updated details of the clothing item</param>
    /// <returns>A <see cref="ClothingItemResponse"/> representing the updated clothing item.</returns>
    Task<ClothingItemResponse> UpdateClothingItemAsync(int id, ClothingItemRequest clothingItemDto);

    /// <summary>
    /// Deletes a clothing item from the database.
    /// </summary>
    /// <param name="id">The id of the clothing item</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteClothingItem(int id);
}
