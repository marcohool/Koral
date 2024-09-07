using System.ComponentModel.DataAnnotations;
using Core.Application.Dtos.ClothingItem;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

public class ClothingItemsController(IClothingItemService clothingItemService) : ApiController
{
    private readonly IClothingItemService clothingItemService = clothingItemService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClothingItemResponseDto>>> GetAllAsync()
    {
        return this.Ok(await this.clothingItemService.GetAllAsync());
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<ClothingItemResponseDto>> GetAsync(Guid id)
    {
        try
        {
            return this.Ok(await this.clothingItemService.GetByIdAsync(id));
        }
        catch (NotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ClothingItemResponseDto>> CreateAsync(
        [FromForm] CreateClothingItemDto createClothingItemDto
    )
    {
        try
        {
            return this.Ok(await this.clothingItemService.CreateAsync(createClothingItemDto));
        }
        catch (ValidationException validationEx)
        {
            return this.BadRequest(validationEx.Message);
        }
    }
}
