using System.ComponentModel.DataAnnotations;

namespace Core.Application.Configuration;

public class MatchingConfiguration
{
    [Required]
    public required double DeltaEThreshold { get; set; }

    [Required]
    public required float CosineSimilarityThreshold { get; set; }

    [Required]
    public int TopN { get; set; }
}
