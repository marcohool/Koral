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
    /// Gets a list of all image uploads.
    /// </summary>
    /// <returns>A list of <see cref="ImageUploadResponse"/> objects.</returns>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ImageUploadResponse>>> GetImageUploads()
    {
        return this.Ok(await this.imageUploadService.GetImageUploadsAsync());
    }

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
    [Route("{id:int}")]
    public async Task<IActionResult> GetImageUpload(int id)
    {
        return this.Ok();
    }
}
