namespace Core.Application.Dtos.ClothingItem;

public record ClothingItemImport : BaseClothingItemDto
{
    public required float[] EmbeddingVector { get; set; }
}
