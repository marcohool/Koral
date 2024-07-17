using Core.Domain.Enums;

namespace Core.Application.Dtos.Upload;

public class UploadResponseDto
{
    public string? Title { get; set; }

    public required string Path { get; set; }

    public UploadStatus Status { get; set; }

    public bool IsFavourited { get; set; }

    public bool IsPinned { get; set; }

    public int? AccuracyRating { get; set; }

    public required string AppUserId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }
}
