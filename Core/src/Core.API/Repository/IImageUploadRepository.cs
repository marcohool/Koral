using Core.API.Models;

namespace Core.API.Repository;

/// <summary>
/// The <see cref="IImageUploadRepository"/> interface.
/// </summary>
public interface IImageUploadRepository
{
    /// <summary>
    /// Gets a list of all of a user's image uploads.
    /// </summary>
    /// <param name="userId">Id of the logged in user.</param>
    /// <returns></returns>
    Task<List<ImageUpload>> GetImageUploads(string userId);

    /// <summary>
    /// Creates a new image upload.
    /// </summary>
    /// <param name="imageUpload">Instance of <see cref="ImageUpload"/>.</param>
    /// <returns></returns>
    Task<ImageUpload> CreateImageUpload(ImageUpload imageUpload);
}
