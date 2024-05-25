using Core.API.Dto.ImageUpload;

namespace Core.API.Services;

/// <summary>
/// The <see cref="IImageUploadService"/> interface.
/// </summary>
public interface IImageUploadService
{
    /// <summary>
    /// Handles image uploading processes.
    /// </summary>
    /// <param name="imageUpload">Instance of <see cref="ImageUploadRequest"/>.</param>
    /// <returns>Instance of <see cref="ImageUploadResponse"/>.</returns>
    Task<ImageUploadResponse> UploadImageAsync(ImageUploadRequest imageUpload);
}
