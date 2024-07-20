using CloudinaryDotNet;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CloudinaryConfiguration = Core.Application.Configuration.CloudinaryConfiguration;

namespace Core.Application.Services;

public class ImageStorageService : IImageStorageService
{
    private readonly Cloudinary cloudinary;

    public ImageStorageService(IOptionsMonitor<CloudinaryConfiguration> cloudinaryConfiguration)
    {
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
