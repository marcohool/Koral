using Core.API.Dto;
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
    /// <returns>A list of <see cref="ClothingItemDto"/> objects</returns>
    Task<IEnumerable<ClothingItemDto>> GetClothingItemsAsync();

    /// <summary>
    /// Gets a clothing item from the database by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ClothingItemDto?> GetClothingItemAsync(int id);
}
