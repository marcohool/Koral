using System.ComponentModel.DataAnnotations;
using Core.Application.Exceptions;
using Core.Application.Models.ClothingItem;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    [HttpPost("publish")]
    public async Task<IActionResult> PublishAsync(PublishClothingItemsRequest request)
    {
        try
        {
            await this.clothingItemService.ImportClothingItems(request.File);

            return this.Ok("Successfully imported");
        }
        catch (JsonSerializationException ex)
        {
            return this.BadRequest(ex.Message);
        }
    }
}
