namespace Core.Domain.Entities;

public class UploadItem
{
    public required Guid UploadId { get; set; }
    public required Upload Upload { get; set; }

    public required string Description { get; set; }

    public required float[] Embedding { get; set; }

    public required List<string> HexColours { get; set; }
}
