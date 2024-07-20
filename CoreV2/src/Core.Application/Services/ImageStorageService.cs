using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Application.Configuration;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CloudinaryConfiguration = Core.Application.Configuration.CloudinaryConfiguration;

namespace Core.Application.Services;

public class ImageStorageService : IImageStorageService
{
    private const string ImageFolder = "images";

    private readonly Cloudinary cloudinary;
    private readonly ImageOptions imageOptions;

    public ImageStorageService(
        IOptionsMonitor<CloudinaryConfiguration> cloudinaryConfiguration,
        IOptionsMonitor<ImageOptions> imageOptions
    )
    {
        this.imageOptions = imageOptions.CurrentValue;

        CloudinaryConfiguration cloudinaryConfigurationValues =
            cloudinaryConfiguration.CurrentValue;

        this.cloudinary = new Cloudinary(
            new Account(
                cloudinaryConfigurationValues.CloudName,
                cloudinaryConfigurationValues.ApiKey,
                cloudinaryConfigurationValues.ApiSecret
            )
        );
    }

    public async Task<string> UploadImageAsync(
        IFormFile image,
        CancellationToken cancellationToken = default
    )
    {
        if (image.Length > this.imageOptions.MaxSize)
        {
            throw new ValidationException("Image size is too large.");
        }

        if (!this.imageOptions.AllowedExtensions.Contains(Path.GetExtension(image.FileName)))
        {
            throw new ValidationException("Invalid file extension.");
        }

        ImageUploadResult result = await this.cloudinary.UploadAsync(
            new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
                Tags = ImageFolder,
            }
        );

        return result.SecureUrl.AbsoluteUri;
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
