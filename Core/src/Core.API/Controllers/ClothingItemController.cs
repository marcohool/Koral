using Core.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ClothingItemController : ControllerBase
{
    private readonly IClothingItemService clothingItemService;

    public ClothingItemController(IClothingItemService clothingItemService)
    {
        this.clothingItemService = clothingItemService;
    }

    [HttpGet]
    public IActionResult GetClothingItems()
    {
        return Ok("Hello from ClothingItemController");
    }
}
