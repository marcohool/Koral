using Core.Application.Dtos;
using Core.Application.Dtos.Upload;

namespace Core.Application.Services.Interfaces;

public interface IUploadService
{
    Task<UploadResponseDto> CreateAsync(
        CreateUploadDto createClothingItemRequestModel,
        CancellationToken cancellationToken = default
    );

    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PaginatedResponse<UploadResponseDto>> GetAllAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task<UploadResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
