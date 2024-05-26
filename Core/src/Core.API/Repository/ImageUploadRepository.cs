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
    public async Task<ImageUpload> CreateImageUpload(ImageUpload imageUpload)
    {
        await this.context.AddAsync(imageUpload);
        await this.context.SaveChangesAsync();

        return imageUpload;
    }
}
