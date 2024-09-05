using Core.Domain.Common;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

public record Upload : BaseEntity, IAuditedEntity
{
    public string? Title { get; set; }

    public required long Size { get; set; }

    public required string ContentType { get; set; }

    public UploadStatus Status { get; set; }

    public bool IsFavourited { get; set; }

    public bool IsPinned { get; set; }

    public int? AccuracyRating { get; set; }

    public bool IsDeleted { get; set; }

    public required string ImageUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }

    public required string AppUserId { get; set; }

    public List<ClothingItem> ClothingItems { get; set; } = [];
}
