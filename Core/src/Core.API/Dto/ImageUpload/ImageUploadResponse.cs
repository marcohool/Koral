using System.Diagnostics.CodeAnalysis;

namespace Core.API.Dto.ImageUpload;

/// <summary>
/// The <see cref="ImageUploadResponse"/> class.
/// </summary>
/// <remarks>
/// Initialises a new instance of the <see cref="ImageUploadResponse"/> class.
/// </remarks>
/// <param name="imageUpload">Instance of <see cref="Models.ImageUpload"/>.</param>
public class ImageUploadResponse(Models.ImageUpload imageUpload)
{
    /// <summary>
    /// Gets or sets the image ID.
    /// </summary>
    public int ImageId { get; set; } = imageUpload.ImageUploadId;

    /// <summary>
    /// Gets or sets the image path.
    /// </summary>
    public string ImagePath { get; set; } = imageUpload.ImagePath;
}
