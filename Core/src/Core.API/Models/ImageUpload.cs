using System.ComponentModel.DataAnnotations.Schema;

namespace Core.API.Models;

/// <summary>
/// The <see cref="ImageUpload"/> class.
/// </summary>
public class ImageUpload
{
    /// <summary>
    /// Image upload id
    /// </summary>
    public int ImageUploadId { get; set; }

    /// <summary>
    /// App user id
    /// </summary>
    [ForeignKey("AppUser")]
    public required string AppUserId { get; set; }

    /// <summary>
    /// Image file name
    /// </summary>
    public required string ImageName { get; set; }

    /// <summary>
    /// Image file path
    /// </summary>
    public required string ImagePath { get; set; }

    /// <summary>
    /// Image file size
    /// </summary>
    public required long ImageSize { get; set; }

    /// <summary>
    /// Image file content type
    /// </summary>
    public required string ContentType { get; set; }

    /// <summary>
    /// Time image was uploaded
    /// </summary>
    public required DateTime UploadedAt { get; set; }

    /// <summary>
    /// Image upload status
    /// </summary>
    public required string Status { get; set; }

    /// <summary>
    /// The app user associated with the image upload
    /// </summary>
    public required AppUser AppUser { get; set; }
}
