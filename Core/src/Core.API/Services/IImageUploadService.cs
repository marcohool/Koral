using Core.API.Dto.ImageUpload;

namespace Core.API.Services;

public interface IImageUploadService
{
    Task<ImageUploadResponse> UploadImageAsync(ImageUploadRequest imageUpload);
}
