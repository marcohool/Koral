using System.ComponentModel.DataAnnotations;

namespace Core.Application.Configuration;

public class ImageOptions
{
    [Required]
    public required int MaxSize { get; set; }

    [Required]
    public required string[] AllowedExtensions { get; set; }
}
