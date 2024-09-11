using Core.Application.Dtos;
using Core.Application.Dtos.Upload;

namespace Core.Application.Services.Interfaces;

public interface IUploadService
{
    Task<UploadDto> CreateAsync(
        CreateUploadDto createClothingItemRequestModel,
        CancellationToken cancellationToken = default
    );

    Task<UploadDto> UpdateAsync(UploadDto uploadDto, CancellationToken cancellationToken = default);

    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PaginatedResponse<UploadDto>> GetAllAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task<PaginatedResponse<UploadDto>> GetFavouritesAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task<UploadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<UploadDto> FavouriteUpload(Guid id);
}
