using Core.API.Dto.ClothingItem;
using Core.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.API.Repository;

/// <summary>
/// The <see cref="ClothingItemRepository"/> class.
/// </summary>
/// <remarks>
/// Initialises a new instance of the <see cref="ClothingItemRepository"/> class.
/// </remarks>
public class ClothingItemRepository(ApplicationDBContext context) : IClothingItemRepository
{
    private readonly ApplicationDBContext context = context;

    /// <inheritdoc />
    public async Task<List<ClothingItemRequest>> GetClothingItemsAsync()
    {
        return await this
            .context.ClothingItems.Select(c => new ClothingItemRequest
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

    /// <inheritdoc />
    public async Task<ClothingItemRequest?> GetClothingItemAsync(int id)
    {
        return await this
            .context.ClothingItems.Where(c => c.Id == id)
            .Select(c => new ClothingItemRequest
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
            .FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<ClothingItem> CreateClothingItemAsync(ClothingItem clothingItem)
    {
        await this.context.AddAsync(clothingItem);
        await this.context.SaveChangesAsync();

        return clothingItem;
    }
}
