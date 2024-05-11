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
            this.context.Update(clothingItem);
            await this.context.SaveChangesAsync();

            return clothingItem;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                "An error occurred while updating the clothing item.",
                e
            );
        }
    }

    /// <inheritdoc />
    public async Task DeleteClothingItem(ClothingItem clothingItem)
    {
        this.context.Remove(clothingItem);
        await this.context.SaveChangesAsync();
    }
}
