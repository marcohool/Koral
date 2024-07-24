using Core.Application.Dtos.ClothingItem;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

public class ClothingItemsController(IClothingItemService clothingItemService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return this.Ok(await clothingItemService.GetAllAsync());
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return this.Ok(await clothingItemService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromForm] CreateClothingItemDto createClothingItemDto
    )
    {
        try
        {
            return this.Ok(await clothingItemService.CreateAsync(createClothingItemDto));
        }
        catch (ValidationException validationEx)
        {
            return this.BadRequest(validationEx.Message);
        }
    }
}