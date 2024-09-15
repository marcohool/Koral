namespace Core.Application.APIs.KoralMatch.Models;

public class ClothingItemEmbedding
{
    public required string Description { get; set; }

    public required string Category { get; set; }

    public required string Colour { get; set; }

    public required float[] EmbeddingVector { get; set; }
}
