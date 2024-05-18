using System.Diagnostics.CodeAnalysis;

namespace Core.API.Dto.ImageUpload;

public class ImageUploadResponse
{
    public bool Success { get; set; }

    public string? ErrorMessage { get; set; }

    public int? ImageId { get; set; }

    public string? ImagePath { get; set; }
}
