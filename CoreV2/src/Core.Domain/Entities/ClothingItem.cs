using Core.Domain.Common;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

public record ClothingItem : BaseEntity, IAuditedEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public required Category Category { get; set; }

    public required List<string>? Colours { get; set; }

    public decimal? Price { get; set; }

    public CurrencyCode? CurrencyCode { get; set; }

    public required Gender Gender { get; set; }

    public string? ImageUrl { get; set; }

    public required string SourceUrl { get; set; }

    public required SourceRegion SourceRegion { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }

    public List<Upload> Uploads { get; set; } = [];
}
