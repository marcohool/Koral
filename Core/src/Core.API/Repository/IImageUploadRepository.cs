using Core.API.Models;

namespace Core.API.Repository;

/// <summary>
/// The <see cref="IImageUploadRepository"/> interface.
/// </summary>
public interface IImageUploadRepository
{
    /// <summary>
    /// Creates a new image upload.
    /// </summary>
    /// <param name="imageUpload">Instance of <see cref="ImageUpload"/>.</param>
    /// <returns></returns>
    Task<ImageUpload> CreateImageUpload(ImageUpload imageUpload);
}
