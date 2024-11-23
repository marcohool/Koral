using Microsoft.AspNetCore.Http;

namespace Core.Application.Models.ClothingItem;

public record PublishClothingItemsRequest
{
    public required IFormFile File { get; set; }
}
