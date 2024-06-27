using Core.API.Models.Enums;

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
    /// Gets or sets the image title.
    /// </summary>
    public string ImageTitle { get; set; } = imageUpload.ImageTitle;

    /// <summary>
    /// Gets or sets the image path.
    /// </summary>
    public string ImagePath { get; set; } = imageUpload.ImagePath;

    /// <summary>
    /// Gets or sets the date the image was uploaded.
    /// </summary>
    public DateTime CreatedAt { get; set; } = imageUpload.CreatedAt;

    /// <summary>
    /// The image upload status
    /// </summary>
    public ImageUploadStatus Status { get; set; } = imageUpload.Status;

    /// <summary>
    /// Whether the image upload is favourited
    /// </summary>
    public bool IsFavourited { get; set; } = imageUpload.IsFavourited;

    /// <summary>
    /// Whether the image upload is pinned
    /// </summary>
    public bool IsPinned { get; set; } = imageUpload.IsPinned;

    /// <summary>
    /// Gets or sets the accuracy rating for the upload.
    /// </summary>
    public int? AccuracyRating { get; set; } = imageUpload.AccuracyRating;

    /// <summary>
    /// Gets or sets the number of clohting items matched for the upload.
    /// </summary>
    public int ClothingItemsMatched { get; set; } = imageUpload.ClothingItemsMatched;
}
