using System.ComponentModel.DataAnnotations;

namespace Core.API.Dto.ImageUpload;

/// <summary>
/// The <see cref="ImageUploadRequest"/> class.
/// </summary>
public class ImageUploadRequest
{
    /// <summary>
    /// Gets or sets the image file.
    /// </summary>
    [Required]
    public required IFormFile ImageFile { get; set; }
}
