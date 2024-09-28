using Core.Domain.Common;

namespace Core.Domain.Entities;

public record UploadItem : BaseEntity
{
    public Guid? UploadId { get; set; }
    public required Upload Upload { get; set; }

    public required string Description { get; set; }

    public required float[] Embedding { get; set; }

    public required string HexColour { get; set; }

    public ICollection<ItemMatch> ItemMatches { get; set; } = [];
}
