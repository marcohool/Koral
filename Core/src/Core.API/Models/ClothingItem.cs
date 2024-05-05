using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.API.Models;

public class ClothingItem
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    public string? Brand { get; set; }

    [Required]
    public required string Category { get; set; }

    [Required]
    public required string Colour { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public decimal? Price { get; set; }

    [Url]
    public string? ImageURL { get; set; }

    [Required]
    [Url]
    public required string SourceURL { get; set; }

    [Required]
    public required DateTime LastChecked { get; set; }
}
