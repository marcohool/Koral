using System.ComponentModel.DataAnnotations;

namespace Core.Application.Configuration;

public class JwtOptions
{
    [Required]
    public required string SigningKey { get; set; }
}
