using Microsoft.AspNetCore.Http;

namespace Core.Application.Dtos.ClothingItem;

public class CreateClothingItemDto : BaseClothingItemDto
{
    public required IFormFile Image { get; set; }
}
