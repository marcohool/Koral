using Microsoft.AspNetCore.Http;

namespace Core.Application.Dtos.ClothingItem;

public record CreateClothingItemDto : BaseClothingItemDto
{
    public required IFormFile Image { get; set; }
}
