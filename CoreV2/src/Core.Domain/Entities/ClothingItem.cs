using Core.Domain.Common;

namespace Core.Domain.Entities;

public class ClothingItem : BaseEntity, IAuditedEntity
{
    public required string Name { get; set; }

    public string? Brand { get; set; }

    public string? Category { get; set; }

    public string? Colour { get; set; }

    public decimal? Price { get; set; }

    public string? ImageUrl { get; set; }

    public required string SourceUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }
}
