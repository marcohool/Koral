using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.API.Dto;

/// <summary>
/// The <see cref="ClothingItemDto"/> class.
/// </summary>
public class ClothingItemDto
{
    /// <summary>
    /// The name of the clothing item.
    /// </summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>
    /// The description of the clothing item.
    /// </summary>
    [Required]
    public required string Description { get; set; }

    /// <summary>
    /// The brand of the clothing item.
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// The category of the clothing item.
    /// </summary>
    [Required]
    public required string Category { get; set; }

    /// <summary>
    /// The colour of the clothing item.
    /// </summary>
    [Required]
    public required string Colour { get; set; }

    /// <summary>
    /// The price of the clothing item.
    /// </summary>
    [Column(TypeName = "decimal(18, 2)")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public decimal? Price { get; set; }

    /// <summary>
    /// The image URL of the clothing item.
    /// </summary>
    [Url]
    public string? ImageURL { get; set; }

    /// <summary>
    /// The source URL of the clothing item.
    /// </summary>
    [Required]
    [Url]
    public required string SourceURL { get; set; }

    /// <summary>
    /// The date the clothing item was last crawled.
    /// </summary>
    [Required]
    public required DateTime LastChecked { get; set; }
}
