using Core.Domain.Enums;

namespace Core.Application.Dtos.Upload;

public class UploadResponseDto
{
    public string? Title { get; set; }

    public UploadStatus Status { get; set; }

    public required string ImageUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }
}
