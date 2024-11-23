using Core.Application.Models.ClothingItem;
using Core.Domain.Enums;

namespace Core.Application.Models.Upload;

public class UploadDto
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public UploadStatus Status { get; set; }

    public required string ImageUrl { get; set; }

    public bool IsFavourited { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }

    public List<ClothingItemResponseDto> MatchedClothingItems { get; set; } = [];
}
