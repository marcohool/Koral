using Core.Domain.Enums;

namespace Core.Application.Models.ItemMatch;

public class CategorisedItemMatches
{
    public required Category Category { get; set; }

    public List<ItemMatchResponseDto> ItemMatches { get; set; } = [];
}
