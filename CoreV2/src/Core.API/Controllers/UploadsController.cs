using System.ComponentModel.DataAnnotations;
using Core.Application.Dtos.Upload;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

[Authorize]
public class UploadsController(IUploadService uploadService) : ApiController
{
    private readonly IUploadService uploadService = uploadService;

    [HttpPost]
    public async Task<ActionResult<UploadResponseDto>> CreateAsync(
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
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UploadResponseDto>>> GetAllAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        return this.Ok(await this.uploadService.GetAllAsync(pageNumber, pageSize));
    }
}
