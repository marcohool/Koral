namespace Core.Application.Models.Matching;

public class MatchResult
{
    public required Domain.Entities.ClothingItem ClothingItem { get; set; }

    public required float EmbeddingSimilarity { get; set; }

    public required double ColourSimilarity { get; set; }
}
