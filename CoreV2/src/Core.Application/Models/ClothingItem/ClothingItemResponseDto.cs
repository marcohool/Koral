namespace Core.Application.Models.ClothingItem;

public record ClothingItemResponseDto : BaseClothingItemDto
{
    public Guid Id { get; set; }
}
