using Core.API.Dto.ImageUpload;
using Core.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

/// <summary>
/// The <see cref="ImageUploadController"/> class.
/// </summary>
/// <param name="imageUploadService"></param>
[Route("[controller]")]
[ApiController]
public class ImageUploadController(IImageUploadService imageUploadService) : ControllerBase
{
    private readonly IImageUploadService imageUploadService = imageUploadService;

    /// <summary>
    /// Uploads an image.
    /// </summary>
    /// <param name="imageUpload"></param>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequest imageUpload)
    {
        try
        {
            ImageUploadResponse response = await this.imageUploadService.UploadImageAsync(
                imageUpload
            );

            if (!response.Success)
                return this.BadRequest(response.ErrorMessage);

            return this.CreatedAtAction(
                nameof(GetImageUpload),
                new { id = response.ImageId },
                response.ImagePath
            );
        }
        catch (Exception e)
        {
            return this.BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetImageUploads()
    {
        return this.Ok();
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetImageUpload(int id)
    {
        return this.Ok();
    }
}
