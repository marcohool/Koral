namespace Core.Application.Dtos.ClothingItem;

public record ClothingItemResponseDto : BaseClothingItemDto
{
    public Guid Id { get; set; }
}
