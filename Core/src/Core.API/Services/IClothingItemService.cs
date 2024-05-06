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
}
