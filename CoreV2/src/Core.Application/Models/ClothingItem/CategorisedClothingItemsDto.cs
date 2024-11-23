using Core.Domain.Enums;

namespace Core.Application.Models.ClothingItem;

public class CategorisedClothingItemsDto
{
    public required Category Category { get; set; }

    public List<ClothingItemResponseDto> ClothingItems { get; set; } = [];
}
