using System.ComponentModel.DataAnnotations;
using Core.Application.Dtos;
using Core.Application.Dtos.Upload;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Core.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

[Authorize]
public class UploadsController(IUploadService uploadService) : ApiController
{
    private readonly IUploadService uploadService = uploadService;

    [HttpPost]
    public async Task<ActionResult<UploadDto>> CreateUploadAsync(
        [FromForm] CreateUploadDto createUploadDto
    )
    {
        try
        {
            return this.Ok(await this.uploadService.CreateAsync(createUploadDto));
        }
        catch (ValidationException validationEx)
        {
            return this.BadRequest(validationEx.Message);
        }
        catch (EmbeddingGenerationException ex)
        {
            return this.StatusCode(
                StatusCodes.Status500InternalServerError,
                $"An error occurred while processing your request. Please try again later. Error: {ex.Message}"
            );
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await this.uploadService.DeleteAsync(id);
            return this.NoContent();
        }
        catch (NotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<UploadDto>>> GetAllUploadsAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        return this.Ok(await this.uploadService.GetAllAsync(pageNumber, pageSize));
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<UploadDto>> GetAsync(Guid id)
    {
        try
        {
            return this.Ok(await this.uploadService.GetByIdAsync(id));
        }
        catch (NotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
    }

    [HttpGet("favourites")]
    public async Task<ActionResult<PaginatedResponse<UploadDto>>> GetFavouriteUploadsAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        return this.Ok(await this.uploadService.GetFavouritesAsync(pageNumber, pageSize));
    }

    [HttpPost("favourite/{id:Guid}")]
    public async Task<ActionResult<UploadDto>> FavouriteUpload(Guid id)
    {
        try
        {
            return this.Ok(await this.uploadService.FavouriteUpload(id));
        }
        catch (NotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
    }
}
