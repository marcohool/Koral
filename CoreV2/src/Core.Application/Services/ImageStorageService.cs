using CloudinaryDotNet;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services;

public class ImageStorageService : IImageStorageService
{
    private readonly Cloudinary cloudinary;

    public ImageStorageService()
    {
        Account account = new Account("dmytroyarmak", "111111111111111", "111111111111111");
    }

    public Task<string> UploadImageAsync(
        IFormFile image,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }

    public Task<string> GetImageUrlAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteImageAsync(string imageUrl, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
