using AutoMapper;
using Core.Application.Dtos.Upload;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Identity;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services;

public class UploadService(
    IMapper mapper,
    IImageStorageService imageStorageService,
    IUploadRepository uploadRepository,
    IClaimService claimService
) : IUploadService
{
    public async Task<UploadResponseDto> CreateAsync(
        CreateUploadDto createUploadDto,
        CancellationToken cancellationToken = default
    )
    {
        IFormFile image = createUploadDto.Image;
        ApplicationUser? user = claimService.GetCurrentUserAsync().Result;

        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        string imageUrl = await imageStorageService.UploadImageAsync(image, cancellationToken);

        Upload upload =
            new()
            {
                AppUserId = user.Id,
                ContentType = image.ContentType,
                Size = image.Length,
                ImageUrl = imageUrl
            };

        Upload createdUpload = await uploadRepository.AddAsync(upload);

        return mapper.Map<UploadResponseDto>(createdUpload);
    }

    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UploadResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }

    public Task<UploadResponseDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }
}
