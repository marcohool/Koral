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
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <returns>A list of <see cref="ImageUpload"/>.</returns>
    Task<List<ImageUpload>> GetImageUploads(string userId, int pageNumber);

    /// <summary>
    /// Gets a single image upload.
    /// </summary>
    /// <param name="userId">Id of the logged in user.</param>
    /// <param name="uploadId">The id of the image upload.</param>
    /// <returns>Instance of <see cref="ImageUpload"/>.</returns>
    Task<ImageUpload?> GetImageUpload(string userId, int uploadId);

    /// <summary>
    /// Creates a new image upload.
    /// </summary>
    /// <param name="imageUpload">Instance of <see cref="ImageUpload"/>.</param>
    /// <returns>Instance of <see cref="ImageUpload"/>.</returns>
    Task<ImageUpload> CreateImageUpload(ImageUpload imageUpload);

    /// <summary>
    /// Updates an image upload.
    /// </summary>
    /// <param name="imageUpload"></param>
    /// <returns>Instance of <see cref="ImageUpload"/>.</returns>
    Task<ImageUpload> UpdateImageUpload(ImageUpload imageUpload);
}
