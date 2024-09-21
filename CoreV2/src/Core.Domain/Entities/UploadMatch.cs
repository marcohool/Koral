using Core.Domain.Common;

namespace Core.Domain.Entities;

public record UploadMatch : BaseEntity, IAuditedEntity
{
    public Guid? UploadId { get; set; }
    public required Upload Upload { get; set; }

    public Guid? ClothingItemId { get; set; }
    public required ClothingItem ClothingItem { get; set; }

    public required string UploadItemDescription { get; set; }

    public required float[] UploadItemEmbedding { get; set; }

    public required float Similarity { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }
}
