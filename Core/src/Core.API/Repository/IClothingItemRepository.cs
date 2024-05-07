using Core.API.Dto;

namespace Core.API.Repository;

/// <summary>
/// Interface for the <see cref="IClothingItemRepository"/> class.
/// </summary>
public interface IClothingItemRepository
{
    /// <summary>
    /// Gets a list of all clothing items from the database.
    /// </summary>
    /// <returns>A list of <see cref="ClothingItemDto"/> objects.</returns>
    Task<List<ClothingItemDto>> GetClothingItemsAsync();

    /// <summary>
    /// Gets a clothing item from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the clothing item.</param>
    /// <returns>The <see cref="ClothingItemDto"/> object if found, otherwise null.</returns>
    Task<ClothingItemDto?> GetClothingItemAsync(int id);
}
