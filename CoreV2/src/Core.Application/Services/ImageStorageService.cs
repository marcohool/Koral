using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Application.Configuration;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Core.Application.Services;

public class ImageStorageService(
    ICloudinaryService cloudinaryService,
    IOptionsMonitor<ImageOptions> imageOptions
) : IImageStorageService
{
    private readonly ICloudinaryService cloudinaryService = cloudinaryService;
    private readonly IOptionsMonitor<ImageOptions> imageOptions = imageOptions;

    private const string ImageFolder = "images";

    public async Task<string> UploadImageAsync(
        IFormFile image,
        CancellationToken cancellationToken = default
    )
    {
        if (image.Length > this.imageOptions.CurrentValue.MaxSize)
        {
            throw new ValidationException("Image size is too large.");
        }

        if (
            !this.imageOptions.CurrentValue.AllowedExtensions.Contains(
                Path.GetExtension(image.FileName)
            )
        )
        {
            throw new ValidationException("Invalid file extension.");
        }

        ImageUploadResult result = await this.cloudinaryService.UploadAsync(
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
