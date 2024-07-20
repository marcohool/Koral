using System.ComponentModel.DataAnnotations;

namespace Core.API.Configuration;

public class CloudinaryConfiguration
{
    [Required]
    public required string CloudName { get; set; }

    [Required]
    public required string ApiKey { get; set; }

    [Required]
    public required string ApiSecret { get; set; }
}
