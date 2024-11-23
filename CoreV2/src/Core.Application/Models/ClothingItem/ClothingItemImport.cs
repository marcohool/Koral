namespace Core.Application.Models.ClothingItem;

public record ClothingItemImport : BaseClothingItemDto
{
    public required float[] EmbeddingVector { get; set; }
}
