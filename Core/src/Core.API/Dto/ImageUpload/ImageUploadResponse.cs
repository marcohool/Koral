using System.Diagnostics.CodeAnalysis;

namespace Core.API.Dto.ImageUpload;

/// <summary>
/// The <see cref="ImageUploadResponse"/> class.
/// </summary>
public class ImageUploadResponse
{
    /// <summary>
    /// Gets or sets the image ID.
    /// </summary>
    public int? ImageId { get; set; }

    /// <summary>
    /// Gets or sets the image path.
    /// </summary>
    public string? ImagePath { get; set; }
}
