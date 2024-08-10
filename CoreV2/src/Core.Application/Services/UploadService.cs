using AutoMapper;
using Core.Application.Dtos.Upload;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Identity;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
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

        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        string imageUrl = await this.imageStorageService.UploadImageAsync(image, cancellationToken);

        Upload upload =
            new()
            {
                AppUserId = user.Id,
                ContentType = image.ContentType,
                Size = image.Length,
                ImageUrl = imageUrl,
            };

        this.uploadRepository.AddAsync(upload);

        return this.mapper.Map<UploadResponseDto>(upload);
    }

    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UploadResponseDto>> GetAllAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        List<Upload> uploads = await this.uploadRepository.GetAllAsync(
            u => u.AppUserId == user.Id,
            pageNumber,
            pageSize
        );

        return this.mapper.Map<IEnumerable<UploadResponseDto>>(uploads);
    }

    public async Task<UploadResponseDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        try
        {
            Upload? upload = await this.uploadRepository.GetFirstAsync(u =>
                u.Id == id && u.AppUserId == user.Id
            );

            return this.mapper.Map<UploadResponseDto>(upload);
        }
        catch (ResourceNotFoundException)
        {
            throw new NotFoundException($"Upload with id {id} not found");
        }
    }
}
