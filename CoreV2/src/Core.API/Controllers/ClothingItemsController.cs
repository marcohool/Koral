using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Core.Application.Dtos.ClothingItem;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

public class ClothingItemsController(
    IClothingItemService clothingItemService,
    IClothingItemParser clothingItemParser
) : ApiController
{
    private readonly IClothingItemService clothingItemService = clothingItemService;
    private readonly IClothingItemParser clothingItemParser = clothingItemParser;

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

    [HttpPost]
    public async Task<IActionResult> PublishAsync(PublishClothingItemsRequest request)
    {
        try
        {
            IEnumerable<ClothingItem> result = await this.clothingItemParser.Parse(request.File);

            // Potential check for if list is empty



            return this.Ok();
        }
        catch (JsonException ex)
        {
            return this.BadRequest(ex.Message);
        }

        throw new NotImplementedException();
    }
}
