using Core.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.API.Repository;

/// <inheritdoc/>
public class ImageUploadRepository(ApplicationDBContext context) : IImageUploadRepository
{
    private readonly ApplicationDBContext context = context;

    /// <inheritdoc/>
    public async Task<List<ImageUpload>> GetImageUploads(string userId)
    {
        return await this.context.ImageUploads.Where(i => i.AppUserId == userId).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<ImageUpload?> GetImageUpload(int id)
    {
        return await this.context.ImageUploads.FirstOrDefaultAsync(i => i.ImageUploadId == id);
    }

    /// <inheritdoc/>
    public async Task<ImageUpload> CreateImageUpload(ImageUpload imageUpload)
    {
        await this.context.AddAsync(imageUpload);
        await this.context.SaveChangesAsync();

        return imageUpload;
    }

    /// <inheritdoc/>
    public async Task<ImageUpload> UpdateImageUpload(ImageUpload imageUpload)
    {
        ImageUpload? existingImage =
            await this.context.ImageUploads.FindAsync(imageUpload.ImageUploadId)
            ?? throw new KeyNotFoundException("Image upload not found");

        existingImage.ImageTitle = imageUpload.ImageTitle;
        existingImage.ImageName = imageUpload.ImageName;
        existingImage.ImagePath = imageUpload.ImagePath;
        existingImage.ImageSize = imageUpload.ImageSize;
        existingImage.ContentType = imageUpload.ContentType;
        existingImage.Status = imageUpload.Status;
        existingImage.IsFavourited = imageUpload.IsFavourited;
        existingImage.IsPinned = imageUpload.IsPinned;
        existingImage.AccuracyRating = imageUpload.AccuracyRating;
        existingImage.ClothingItemsMatched = imageUpload.ClothingItemsMatched;
        existingImage.IsDeleted = imageUpload.IsDeleted;

        await this.context.SaveChangesAsync();

        return existingImage;
    }
}
