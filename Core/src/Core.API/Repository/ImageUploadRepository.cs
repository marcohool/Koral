using Core.API.Models;

namespace Core.API.Repository;

/// <inheritdoc/>
public class ImageUploadRepository(ApplicationDBContext context) : IImageUploadRepository
{
    private readonly ApplicationDBContext context = context;

    /// <inheritdoc/>
    public async Task<ImageUpload> CreateImageUpload(ImageUpload imageUpload)
    {
        await this.context.AddAsync(imageUpload);
        await this.context.SaveChangesAsync();

        return imageUpload;
    }
}
