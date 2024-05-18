using System.ComponentModel.DataAnnotations.Schema;

namespace Core.API.Models;

/// <summary>
/// The <see cref="ImageUpload"/> class.
/// </summary>
public class ImageUpload
{
    public int ImageUploadId { get; set; }

    [ForeignKey("AppUser")]
    public string AppUserId { get; set; }

    public string Image { get; set; }

    public DateTime UploadedAt { get; set; }

    public string Status { get; set; }

    public required AppUser AppUser { get; set; }
}
