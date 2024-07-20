using Microsoft.AspNetCore.Http;

namespace Core.Application.Dtos.ClothingItem;

public class ClothingItemResponseDto : BaseClothingItemDto
{
    public Guid Id { get; set; }

    public required string ImageUrl { get; set; }
}
