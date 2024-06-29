﻿using Core.API.Dto.ImageUpload;

namespace Core.API.Services;

/// <summary>
/// The <see cref="IImageUploadService"/> interface.
/// </summary>
public interface IImageUploadService
{
    /// <summary>
    /// Gets a list of all image uploads.
    /// </summary>
    /// <returns>A list of <see cref="ImageUploadResponse"/> objects.</returns>
    Task<IEnumerable<ImageUploadResponse>> GetImageUploadsAsync();

    /// <summary>
    /// Handles image uploading processes.
    /// </summary>
    /// <param name="imageUpload">Instance of <see cref="ImageUploadRequest"/>.</param>
    /// <returns>Instance of <see cref="ImageUploadResponse"/>.</returns>
    Task<ImageUploadResponse> UploadImageAsync(ImageUploadRequest imageUpload);

    /// <summary>
    /// Favourites an image upload.
    /// </summary>
    /// <param name="id">The id of the image upload</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task FavouriteImageUploadAsync(int id);
}
