namespace Core.Application.Dtos.ClothingItem;

public class BaseClothingItemDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public string? Category { get; set; }

    public string? Colour { get; set; }

    public decimal? Price { get; set; }

    public required string SourceUrl { get; set; }
}
