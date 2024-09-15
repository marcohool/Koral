using System.ComponentModel.DataAnnotations;

namespace Core.Application.Configuration;

public class KoralMatchConfiguration
{
    [Required]
    public required string BaseUri { get; set; }
}
