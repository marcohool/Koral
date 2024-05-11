using Core.API.Dto.ClothingItem;
using Core.API.Services;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    /// <returns>A list of <see cref="ClothingItemRequest"/>.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClothingItemRequest>>> GetClothingItems()
    {
        return this.Ok(await this.clothingItemService.GetClothingItemsAsync());
    }

    /// <summary>
    /// Gets a clothing item from the database by its ID.
    /// </summary>
    /// <param name="id">Id of the clothing item.</param>
    /// <returns>Instance of <see cref="ClothingItemRequest"/>.</returns>
    [HttpGet("{id:int}", Name = "GetClothingItem")]
    public async Task<ActionResult<ClothingItemResponse>> GetClothingItem(int id)
    {
        ClothingItemResponse? clothingItem = await this.clothingItemService.GetClothingItemAsync(
            id
        );

        if (clothingItem is null)
        {
            return this.NotFound();
        }

        return this.Ok(clothingItem);
    }

    /// <summary>
    /// Creates a new clothing item in the database.
    /// </summary>
    /// <param name="clothingItemRequest">Instance of <see cref="ClothingItemRequest"/>.</param>
    /// <returns>The created clothing item.</returns>
    [HttpPost]
    public async Task<ActionResult<ClothingItemResponse>> CreateClothingItem(
        [FromBody] ClothingItemRequest clothingItemRequest
    )
    {
        try
        {
            ClothingItemResponse clothingItemResponse =
                await this.clothingItemService.CreateClothingItemAsync(clothingItemRequest);

            return this.CreatedAtRoute(
                "GetClothingItem",
                new { id = clothingItemResponse.Id },
                clothingItemResponse
            );
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates a clothing item in the database.
    /// </summary>
    /// <param name="id">The id of the clothing item.</param>
    /// <param name="clothingItemRequest">The updated details of the clothing item</param>
    /// <returns>The updated clothing item.</returns>
    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult<ClothingItemResponse>> UpdateClothingItem(
        int id,
        [FromBody] ClothingItemRequest clothingItemRequest
    )
    {
        try
        {
            return this.Ok(
                await this.clothingItemService.UpdateClothingItemAsync(id, clothingItemRequest)
            );
        }
        catch (KeyNotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a clothing item from the database.
    /// </summary>
    /// <param name="id">The id of the clothing item.</param>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteClothingItem(int id)
    {
        try
        {
            await this.clothingItemService.DeleteClothingItem(id);

            return this.NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }
}
