﻿using Core.API.Dto;
using Core.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.API.Repository;

/// <summary>
/// The <see cref="ClothingItemRepository"/> class.
/// </summary>
/// <remarks>
/// Initialises a new instance of the <see cref="ClothingItemRepository"/> class.
/// </remarks>
/// <param name="context"></param>
public class ClothingItemRepository(ApplicationDBContext context) : IClothingItemRepository
{
    private readonly ApplicationDBContext context = context;

    /// <summary>
    /// Gets a list of all clothing items from the database.
    /// </summary>
    /// <returns></returns>
    public Task<List<ClothingItemDto>> GetClothingItemsAsync()
    {
        return this
            .context.ClothingItems.Select(c => new ClothingItemDto
            {
                Name = c.Name,
                Description = c.Description,
                Brand = c.Brand,
                Category = c.Category,
                Colour = c.Colour,
                Price = c.Price,
                ImageURL = c.ImageURL,
                SourceURL = c.SourceURL,
                LastChecked = c.LastChecked
            })
            .ToListAsync();
    }
}
