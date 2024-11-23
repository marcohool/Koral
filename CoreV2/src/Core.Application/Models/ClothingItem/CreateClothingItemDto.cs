using Microsoft.AspNetCore.Http;

namespace Core.Application.Models.ClothingItem;

public record CreateClothingItemDto : BaseClothingItemDto
{
    public required IFormFile Image { get; set; }
}
