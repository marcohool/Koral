using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.Upload;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Identity;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

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

        await this.uploadRepository.AddAsync(upload);

        return this.mapper.Map<UploadResponseDto>(upload);
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        Upload? upload = await this.uploadRepository.GetFirstAsync(u =>
            u.Id == id && u.AppUserId == user.Id
        );

        if (upload is null)
        {
            throw new NotFoundException($"Upload with id {id} not found");
        }

        IDbContextTransaction transaction = await this.uploadRepository.BeginTransactionAsync();

        Guid uploadGuid = await this.uploadRepository.DeleteAsync(upload);

        await this.imageStorageService.DeleteImageAsync(upload.ImageUrl, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return uploadGuid;
    }

    public async Task<PaginatedResponse<UploadResponseDto>> GetAllAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        int totalUploads = await this.uploadRepository.CountAsync(u => u.AppUserId == user.Id);

        List<Upload> uploads = await this.uploadRepository.GetAllAsync(
            u => u.AppUserId == user.Id,
            pageNumber,
            pageSize
        );

        return new PaginatedResponse<UploadResponseDto>(
            this.mapper.Map<IEnumerable<UploadResponseDto>>(uploads),
            pageNumber,
            pageSize,
            totalUploads
        );
    }

    public async Task<PaginatedResponse<UploadResponseDto>> GetFavouritesAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        int totalUploads = await this.uploadRepository.CountAsync(u =>
            u.AppUserId == user.Id && u.IsFavourited
        );

        List<Upload> uploads = await this.uploadRepository.GetAllAsync(
            u => u.AppUserId == user.Id && u.IsFavourited,
            pageNumber,
            pageSize
        );

        return new PaginatedResponse<UploadResponseDto>(
            this.mapper.Map<IEnumerable<UploadResponseDto>>(uploads),
            pageNumber,
            pageSize,
            totalUploads
        );
    }

    public async Task<UploadResponseDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        Upload upload = await this.GetUserUpload(id);

        return this.mapper.Map<UploadResponseDto>(upload);
    }

    public async Task<UploadResponseDto> FavouriteUpload(Guid id)
    {
        Upload upload = await this.GetUserUpload(id);

        upload.IsFavourited = !upload.IsFavourited;

        return this.mapper.Map<UploadResponseDto>(await this.uploadRepository.UpdateAsync(upload));
    }

    private async Task<Upload> GetUserUpload(Guid id)
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        Upload? upload = await this.uploadRepository.GetFirstAsync(u =>
            u.Id == id && u.AppUserId == user.Id
        );

        if (upload is null)
        {
            throw new NotFoundException($"Upload with id {id} not found");
        }

        return upload;
    }
}
