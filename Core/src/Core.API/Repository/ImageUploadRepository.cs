using Core.API.Models;

namespace Core.API.Repository;

public class ImageUploadRepository(ApplicationDBContext context) : IImageUploadRepository
{
    private readonly ApplicationDBContext context = context;

    public async Task<ImageUpload> CreateImageUpload(ImageUpload imageUpload)
    {
        await this.context.AddAsync(imageUpload);
        await this.context.SaveChangesAsync();

        return imageUpload;
    }
}
