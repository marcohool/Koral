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
    /// <returns>A list of <see cref="ClothingItem"/> objects.</returns>
    Task<List<ClothingItem>> GetClothingItemsAsync();

    /// <summary>
    /// Gets a clothing item from the database by its ID.
    /// </summary>
    /// <param name="id">The id of the clothing item.</param>
    /// <returns>The <see cref="ClothingItem"/> object if found, otherwise null.</returns>
    Task<ClothingItem?> GetClothingItemAsync(int id);

    /// <summary>
    /// Creates a new clothing item in the database.
    /// </summary>
    /// <param name="clothingItem">Instance of <see cref="ClothingItem"/>.</param>
    /// <returns>The created <see cref="ClothingItem"/>.</returns>
    Task<ClothingItem> CreateClothingItemAsync(ClothingItem clothingItem);

    /// <summary>
    /// Updates a clothing item in the database.
    /// </summary>
    /// <param name="clothingItem">Instance of <see cref="ClothingItem"/>.</param>
    /// <returns>The updated <see cref="ClothingItem"/>.</returns>
    Task<ClothingItem> UpdateClothingItemAsync(ClothingItem clothingItem);

    /// <summary>
    /// Deletes a clothing item from the database.
    /// </summary>
    /// <param name="clothingItem">Instance of <see cref="ClothingItem"/>.</param>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task DeleteClothingItem(ClothingItem clothingItem);
}
