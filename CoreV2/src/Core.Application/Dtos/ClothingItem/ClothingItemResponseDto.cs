using Microsoft.AspNetCore.Http;

namespace Core.Application.Dtos.ClothingItem;

public class ClothingItemResponseDto : BaseResponseDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public string? Category { get; set; }

    public string? Colour { get; set; }

    public decimal? Price { get; set; }

    public required IFormFile Image { get; set; }

    public required string SourceUrl { get; set; }
}
