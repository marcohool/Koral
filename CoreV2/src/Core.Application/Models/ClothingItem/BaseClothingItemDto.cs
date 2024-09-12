using Core.Domain.Enums;

namespace Core.Application.Dtos.ClothingItem;

public record BaseClothingItemDto
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public string? Brand { get; set; }

    public required Category Category { get; set; }

    public required List<string> Colours { get; set; }

    public decimal? Price { get; set; }

    public required CurrencyCode CurrencyCode { get; set; }

    public required Gender Gender { get; set; } = Gender.Unknown;

    public string? ImageUrl { get; set; }

    public required string SourceUrl { get; set; }

    public required SourceRegion SourceRegion { get; set; }

    public required float[] EmbeddingVector { get; set; }
}
