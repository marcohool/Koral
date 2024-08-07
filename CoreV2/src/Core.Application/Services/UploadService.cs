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
    private readonly IMapper mapper = mapper;
    private readonly IImageStorageService imageStorageService = imageStorageService;
    private readonly IUploadRepository uploadRepository = uploadRepository;
    private readonly IClaimService claimService = claimService;

    public async Task<UploadResponseDto> CreateAsync(
        CreateUploadDto createUploadDto,
        CancellationToken cancellationToken = default
    )
    {
        IFormFile image = createUploadDto.Image;

        ApplicationUser? user = await this.claimService.GetCurrentUserAsync();

        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        string imageUrl = await this.imageStorageService.UploadImageAsync(image, cancellationToken);

        Upload upload =
            new()
            {
                AppUserId = user.Id,
                ContentType = image.ContentType,
                Size = image.Length,
                ImageUrl = imageUrl,
            };

        Upload createdUpload = await this.uploadRepository.AddAsync(upload);

        return this.mapper.Map<UploadResponseDto>(createdUpload);
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
