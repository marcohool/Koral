using Core.Application.Models.ClothingItem;

namespace Core.Application.Models.ItemMatch;

public record ItemMatchResponseDto : BaseClothingItemDto
{
    public Guid ClothingItemId { get; set; }

    public float EmbeddingSimilarity { get; set; }

    public double ColourSimilarity { get; set; }

    public float OverallSimilarity { get; set; }
}
