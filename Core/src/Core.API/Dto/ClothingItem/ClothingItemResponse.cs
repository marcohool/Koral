﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Core.API.Dto.ClothingItem;

/// <summary>
/// The <see cref="ClothingItemResponse"/> record.
/// </summary>
public record ClothingItemResponse
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ClothingItemResponse"/> class.
    /// </summary>
    /// <param name="clothingItem">Instance of <see cref="Models.ClothingItem"/>.</param>
    [SetsRequiredMembers]
    public ClothingItemResponse(Models.ClothingItem clothingItem)
    {
        this.Id = clothingItem.ClothingItemId;
        this.Name = clothingItem.Name;
        this.Description = clothingItem.Description;
        this.Brand = clothingItem.Brand;
        this.Category = clothingItem.Category;
        this.Colour = clothingItem.Colour;
        this.Price = clothingItem.Price;
        this.ImageURL = clothingItem.ImageURL;
        this.SourceURL = clothingItem.SourceURL;
        this.LastChecked = clothingItem.LastChecked;
    }

    /// <summary>
    /// The ID of the clothing item.
    /// </summary>
    public required int Id { get; set; }

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

    /// <summary>
    /// Converts the <see cref="ClothingItemResponse"/> to a <see cref="Models.ClothingItem"/>.
    /// </summary>
    /// <returns><see cref="Models.ClothingItem"/> object.</returns>
    public Models.ClothingItem ToClothingItemModel() =>
        new()
        {
            Name = this.Name,
            Description = this.Description,
            Brand = this.Brand,
            Category = this.Category,
            Colour = this.Colour,
            Price = this.Price,
            ImageURL = this.ImageURL,
            SourceURL = this.SourceURL,
            LastChecked = this.LastChecked
        };
}
