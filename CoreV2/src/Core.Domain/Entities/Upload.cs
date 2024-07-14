using Core.Domain.Common;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

public class Upload : BaseEntity, IAuditedEntity
{
    public string? Title { get; set; }

    public required string Path { get; set; }

    public required long Size { get; set; }

    public required string ContentType { get; set; }

    public UploadStatus Status { get; set; } = UploadStatus.Processing;

    public required string AppUserId { get; set; }

    public required AppUser AppUser { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }
}
