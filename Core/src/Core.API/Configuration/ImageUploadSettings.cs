namespace Core.API.Configuration;

/// <summary>
/// The <see cref="ImageUploadSettings"/> class.
/// </summary>
public class ImageUploadSettings
{
    /// <summary>
    /// Gets or sets the maximum file size.
    /// </summary>
    public required long MaxFileSize { get; set; }

    /// <summary>
    /// Gets or sets the allowed file extensions.
    /// </summary>
    public required string[] AllowedFileExtensions { get; set; }
}
