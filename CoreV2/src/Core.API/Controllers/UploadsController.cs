using System.ComponentModel.DataAnnotations;
using Core.Application.Dtos.Upload;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

[Authorize]
public class UploadsController(IUploadService uploadService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CreateUploadDto createUploadDto)
    {
        try
        {
            return this.Ok(await uploadService.CreateAsync(createUploadDto));
        }
        catch (ValidationException validationEx)
        {
            return this.BadRequest(validationEx.Message);
        }
    }
}
