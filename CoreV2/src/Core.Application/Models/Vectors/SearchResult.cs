namespace Core.Application.Models.Vectors;

public class SearchResult
{
    public required Guid Id { get; set; }

    public float Similarity { get; set; }
}
