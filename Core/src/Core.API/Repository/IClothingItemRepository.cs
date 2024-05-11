using Core.API.Dto.ClothingItem;
using Core.API.Models;

namespace Core.API.Repository;

/// <summary>
/// Interface for the <see cref="IClothingItemRepository"/> class.
/// </summary>
public interface IClothingItemRepository
{
    /// <summary>
    /// Gets a list of all clothing items from the database.
    /// </summary>
    /// <returns>A list of <see cref="ClothingItemRequest"/> objects.</returns>
    Task<List<ClothingItemRequest>> GetClothingItemsAsync();

    /// <summary>
    /// Gets a clothing item from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the clothing item.</param>
    /// <returns>The <see cref="ClothingItemRequest"/> object if found, otherwise null.</returns>
    Task<ClothingItemRequest?> GetClothingItemAsync(int id);

    /// <summary>
    /// Creates a new clothing item in the database.
    /// </summary>
    /// <param name="clothingItem">Instance of <see cref="ClothingItem"/>.</param>
    /// <returns>The created clothing item.</returns>
    Task<ClothingItem> CreateClothingItemAsync(ClothingItem clothingItem);
}
