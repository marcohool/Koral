using System.ComponentModel.DataAnnotations;

namespace Core.Application.Configuration;

public class JwtOptions
{
    [Required]
    public required string Issuer { get; set; }

    [Required]
    public required string Audience { get; set; }

    [Required]
    [MinLength(128)]
    public required string SigningKey { get; set; }
}
