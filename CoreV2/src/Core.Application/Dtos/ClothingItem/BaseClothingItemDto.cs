using Core.Domain.Enums;

namespace Core.Application.Dtos.ClothingItem;

public record BaseClothingItemDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public string? Category { get; set; }

    public string? Colour { get; set; }

    public decimal? Price { get; set; }

    public required string CurrencyCode { get; set; }

    public Gender Gender { get; set; }

    public required string SourceUrl { get; set; }

    public SourceRegion SourceRegion { get; set; }
}
