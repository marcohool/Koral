using System.ComponentModel.DataAnnotations.Schema;

namespace Core.API.Models;

/// <summary>
/// The <see cref="ImageUpload"/> class.
/// </summary>
public class ImageUpload
{
    public int ImageUploadId { get; set; }

    [ForeignKey("AppUser")]
    public required string AppUserId { get; set; }

    public required string ImageName { get; set; }

    public required string ImagePath { get; set; }

    public required long ImageSize { get; set; }

    public string ContentType { get; set; }

    public required DateTime UploadedAt { get; set; }

    public required string Status { get; set; }

    public required AppUser AppUser { get; set; }
}
