using System.Drawing;
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
    public async Task<List<ClothingItem>> GetClothingItemsAsync()
    {
        return await this.context.ClothingItems.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<ClothingItem?> GetClothingItemAsync(int id)
    {
        return await this.context.ClothingItems.Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<ClothingItem> CreateClothingItemAsync(ClothingItem clothingItem)
    {
        await this.context.AddAsync(clothingItem);
        await this.context.SaveChangesAsync();

        return clothingItem;
    }

    /// <inheritdoc />
    public async Task<ClothingItem> UpdateClothingItemAsync(ClothingItem clothingItem)
    {
        try
        {
            ClothingItem dbClothingItem = this.context.ClothingItems.First(x =>
                x.Id == clothingItem.Id
            );

            dbClothingItem.Name = clothingItem.Name;
            dbClothingItem.Description = clothingItem.Description;
            dbClothingItem.Brand = clothingItem.Brand;
            dbClothingItem.Category = clothingItem.Category;
            dbClothingItem.Colour = clothingItem.Colour;
            dbClothingItem.Price = clothingItem.Price;
            dbClothingItem.ImageURL = clothingItem.ImageURL;
            dbClothingItem.SourceURL = clothingItem.SourceURL;
            dbClothingItem.LastChecked = clothingItem.LastChecked;

            this.context.Update(dbClothingItem);
            await this.context.SaveChangesAsync();

            return dbClothingItem;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                "An error occurred while updating the clothing item.",
                e
            );
        }
    }
}
