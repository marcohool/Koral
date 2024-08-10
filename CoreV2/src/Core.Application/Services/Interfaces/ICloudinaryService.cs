using CloudinaryDotNet.Actions;

namespace Core.Application.Services.Interfaces;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadAsync(ImageUploadParams uploadParams);

    Task<DeletionResult> DeleteAsync(DeletionParams deletionParams);
}
