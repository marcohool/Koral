using Microsoft.AspNetCore.Http;

namespace Core.Application.Dtos.ClothingItem;

public record PublishClothingItemsRequest
{
    public required IFormFile File { get; set; }
}
