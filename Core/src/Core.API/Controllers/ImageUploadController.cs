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
[Authorize]
[ApiController]
public class ImageUploadController(IImageUploadService imageUploadService) : ControllerBase
{
    private readonly IImageUploadService imageUploadService = imageUploadService;

    /// <summary>
    /// Gets a list of all image uploads.
    /// </summary>
    /// <returns>A list of <see cref="ImageUploadResponse"/> objects.</returns>
    [HttpGet]
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

    /// <summary>
    /// Favourites an image upload.
    /// </summary>
    /// <param name="id">The id of the image to favourite</param>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    [HttpPut]
    [Route("favourite/{id:int}")]
    public async Task<IActionResult> FavouriteImageUpload(int id)
    {
        try
        {
            await this.imageUploadService.FavouriteImageUploadAsync(id);
        }
        catch (KeyNotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return this.Unauthorized(ex.Message);
        }

        return this.Ok();
    }

    /// <summary>
    /// Gets a list of all favourite image uploads.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("favourites")]
    public async Task<ActionResult<IEnumerable<ImageUploadResponse>>> GetFavouriteImageUploads()
    {
        return this.Ok(await this.imageUploadService.GetFavouriteImageUploads());
    }
}
