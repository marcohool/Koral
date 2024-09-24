namespace Core.Domain.Entities;

public record ItemMatch
{
    public Guid? UploadItemId { get; set; }
    public required UploadItem UploadItem { get; set; }

    public Guid? ClothingItemId { get; set; }
    public required ClothingItem ClothingItem { get; set; }

    public required float EmbeddingSimilarity { get; set; }

    public required float ColourSimilarity { get; set; }
}
