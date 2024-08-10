using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using CloudinaryConfiguration = Core.Application.Configuration.CloudinaryConfiguration;

namespace Core.Application.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary cloudinary;

    public CloudinaryService(IOptionsMonitor<CloudinaryConfiguration> cloudinaryConfiguration)
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

    public Task<ImageUploadResult> UploadAsync(ImageUploadParams uploadParams)
    {
        return this.cloudinary.UploadAsync(uploadParams);
    }

    public Task<DeletionResult> DeleteAsync(DeletionParams deletionParams)
    {
        return this.cloudinary.DestroyAsync(deletionParams);
    }
}
