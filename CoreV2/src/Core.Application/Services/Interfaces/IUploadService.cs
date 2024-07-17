using Core.Application.Dtos.Upload;

namespace Core.Application.Services.Interfaces;

public interface IUploadService
{
    Task<UploadResponseDto> CreateAsync(
        CreateUploadDto createClothingItemRequestModel,
        CancellationToken cancellationToken = default
    );

    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<UploadResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<UploadResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
