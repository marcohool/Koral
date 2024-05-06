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
}
