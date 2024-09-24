using Core.Domain.Common;

namespace Core.Domain.Entities;

public record UploadItem : BaseEntity
{
    public required Guid UploadId { get; set; }
    public required Upload Upload { get; set; }

    public required string Description { get; set; }

    public required float[] Embedding { get; set; }

    public required List<string> HexColours { get; set; }

    public required ICollection<ItemMatch> ItemMatches { get; set; }
}
