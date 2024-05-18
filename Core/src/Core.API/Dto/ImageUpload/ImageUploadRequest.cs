using System.ComponentModel.DataAnnotations;

namespace Core.API.Dto.ImageUpload;

public class ImageUploadRequest
{
    [Required]
    public required IFormFile ImageFile { get; set; }
}
