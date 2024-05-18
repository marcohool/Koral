using Core.API.Models;

namespace Core.API.Repository;

public interface IImageUploadRepository
{
    Task<ImageUpload> CreateImageUpload(ImageUpload imageUpload);
}
