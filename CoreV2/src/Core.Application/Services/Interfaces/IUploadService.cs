using Core.Application.Dtos.Upload;

namespace Core.Application.Services.Interfaces;

public interface IUploadService
{
    Task<UploadDto> CreateAsync(
        CreateUploadDto createClothingItemRequestModel,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<UploadDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<UploadDto>> GetFavouritesAsync(CancellationToken cancellationToken = default);

    Task<UploadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task FavouriteUpload(Guid id);
}
