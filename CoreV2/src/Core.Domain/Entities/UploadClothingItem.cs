using Core.Domain.Common;

namespace Core.Domain.Entities;

public record UploadClothingItem : BaseEntity, IAuditedEntity
{
    public required Guid UploadId { get; set; }
    public required Upload Upload { get; set; }

    public required Guid ClothingItemId { get; set; }
    public required ClothingItem ClothingItem { get; set; }

    public required string UploadItemDescription { get; set; }

    public required float[] UploadItemEmbedding { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }
}
