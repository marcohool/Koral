namespace Core.Domain.Entities;

public record UploadMatch
{
    public Guid? UploadId { get; set; }
    public required Upload Upload { get; set; }

    public Guid? ClothingItemId { get; set; }
    public required ClothingItem ClothingItem { get; set; }

    public required string UploadItemDescription { get; set; }

    public required float[] UploadItemEmbedding { get; set; }

    public required float Similarity { get; set; }
}
