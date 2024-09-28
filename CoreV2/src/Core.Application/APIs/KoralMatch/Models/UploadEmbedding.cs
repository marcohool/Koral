namespace Core.Application.APIs.KoralMatch.Models;

public record UploadEmbedding
{
    public required string Title { get; set; }

    public List<ItemEmbedding>? ItemEmbeddings { get; set; }
}
