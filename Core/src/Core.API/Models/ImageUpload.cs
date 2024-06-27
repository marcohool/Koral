using System.ComponentModel.DataAnnotations.Schema;
using Core.API.Models.Enums;

namespace Core.API.Models;

/// <summary>
/// The <see cref="ImageUpload"/> class.
/// </summary>
public class ImageUpload
{
    /// <summary>
    /// THe image upload id
    /// </summary>
    public int ImageUploadId { get; set; }

    /// <summary>
    /// The app user id
    /// </summary>
    [ForeignKey("AppUser")]
    public required string AppUserId { get; set; }

    /// <summary>
    /// The image title
    /// </summary>
    public string ImageTitle { get; set; } = string.Empty;

    /// <summary>
    /// The image file name
    /// </summary>
    public required string ImageName { get; set; }

    /// <summary>
    /// The image file path
    /// </summary>
    public required string ImagePath { get; set; }

    /// <summary>
    /// The image file size
    /// </summary>
    public required long ImageSize { get; set; }

    /// <summary>
    /// The image file content type
    /// </summary>
    public required string ContentType { get; set; }

    /// <summary>
    /// THe time the image was uploaded
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The image upload status
    /// </summary>
    public ImageUploadStatus Status { get; set; } = ImageUploadStatus.Processing;

    /// <summary>
    /// The app user associated with the image upload
    /// </summary>
    public required AppUser AppUser { get; set; }

    /// <summary>
    /// Whether the image upload is favourited
    /// </summary>
    public bool IsFavourited { get; set; } = false;

    /// <summary>
    /// Whether the image upload is pinned
    /// </summary>
    public bool IsPinned { get; set; } = false;

    /// <summary>
    /// The number of clothing items matched for the upload
    /// </summary>
    public int ClothingItemsMatched { get; set; } = 0;

    /// <summary>
    /// Whether the image upload is deleted
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}
