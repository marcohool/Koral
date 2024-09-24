using Core.Domain.Common;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

public record ClothingItem : BaseEntity, IAuditedEntity
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public string? Brand { get; set; }

    public required Category Category { get; set; }

    public required List<string>? Colours { get; set; }

    public required decimal Price { get; set; }

    public required CurrencyCode CurrencyCode { get; set; }

    public required Gender Gender { get; set; } = Gender.Unknown;

    public required string ImageUrl { get; set; }

    public required string SourceUrl { get; set; }

    public required SourceRegion SourceRegion { get; set; }

    public required float[] EmbeddingVector { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }

    public ICollection<ItemMatch> UploadMatches { get; set; } = [];
}
