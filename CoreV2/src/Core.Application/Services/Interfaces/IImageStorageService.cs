using Microsoft.AspNetCore.Http;

namespace Core.Application.Services.Interfaces;

public interface IImageStorageService
{
    Task<string> UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default);

    Task<string> GetImageUrlAsync(Guid id, CancellationToken cancellationToken = default);

    Task DeleteImageAsync(string imageUrl, CancellationToken cancellationToken = default);
}
