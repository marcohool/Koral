using Core.API.Dto;
using Core.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

/// <summary>
/// The <see cref="ClothingItemController"/> class."
/// </summary>
/// <remarks>
/// Initialises a new instace of the <see cref="ClothingItemController"/> class.
/// </remarks>
/// <param name="clothingItemService"></param>
[Route("[controller]")]
[ApiController]
public class ClothingItemController(IClothingItemService clothingItemService) : ControllerBase
{
    private readonly IClothingItemService clothingItemService = clothingItemService;

    /// <summary>
    /// Gets a list of all clothing items from the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClothingItemDto>>> GetClothingItems()
    {
        return this.Ok(await this.clothingItemService.GetClothingItemsAsync());
    }
}
