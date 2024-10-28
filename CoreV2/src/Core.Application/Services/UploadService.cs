using AutoMapper;
using Core.Application.APIs.KoralMatch;
using Core.Application.APIs.KoralMatch.Models;
using Core.Application.Dtos.ClothingItem;
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
    IClaimService claimService,
    IKoralMatchApi koralMatchApi,
    IMatchingService matchingService,
    IItemMatchRepository uploadMatchesRepository,
    IUploadItemRepository uploadItemRepository
) : IUploadService
{
    private readonly IMapper mapper = mapper;
    private readonly IImageStorageService imageStorageService = imageStorageService;
    private readonly IUploadRepository uploadRepository = uploadRepository;
    private readonly IClaimService claimService = claimService;
    private readonly IKoralMatchApi koralMatchApi = koralMatchApi;
    private readonly IMatchingService matchingService = matchingService;
    private readonly IItemMatchRepository uploadMatchesRepository = uploadMatchesRepository;
    private readonly IUploadItemRepository uploadItemRepository = uploadItemRepository;

    public async Task<UploadDto> CreateAsync(
        CreateUploadDto createClothingItemRequestModel,
        CancellationToken cancellationToken = default
    )
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        IFormFile image = createClothingItemRequestModel.Image;

        UploadEmbedding uploadEmbedding = await this.koralMatchApi.GetUploadEmbedding(image);

        //string imageUrl = await this.imageStorageService.UploadImageAsync(image, cancellationToken);
        string imageUrl = "www.test.com/image";

        Upload upload =
            new()
            {
                Title = uploadEmbedding.Title,
                ImageUrl = imageUrl,
                AppUserId = user.Id
            };

        await this.uploadRepository.AddAsync(upload);

        List<ItemMatch> allMatches = [];

        foreach (ItemEmbedding itemEmbedding in uploadEmbedding.ItemEmbeddings ?? [])
        {
            UploadItem uploadItem =
                new()
                {
                    Description = itemEmbedding.Description,
                    Embedding = itemEmbedding.EmbeddingVector,
                    HexColour = itemEmbedding.Colour,
                    Upload = upload
                };

            await this.uploadItemRepository.AddAsync(uploadItem);

            IEnumerable<ItemMatch> matches = (
                await this.matchingService.GetMatches(itemEmbedding, cancellationToken)
            ).Select(x => new ItemMatch()
            {
                UploadItem = uploadItem,
                ClothingItem = x.ClothingItem,
                EmbeddingSimilarity = x.EmbeddingSimilarity,
                ColourSimilarity = x.ColourSimilarity
            });

            await this.uploadMatchesRepository.AddRangeAsync(matches);
            allMatches.AddRange(matches);
        }

        UploadDto uploadDto = this.mapper.Map<UploadDto>(upload);
        uploadDto.MatchedClothingItems =
            allMatches.Count > 0
                ? this.mapper.Map<List<ClothingItemResponseDto>>(
                    allMatches.Select(x => x.ClothingItem)
                )
                : [];

        return uploadDto;
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

    public async Task<IEnumerable<UploadDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        int totalUploads = await this.uploadRepository.CountAsync(u => u.AppUserId == user.Id);

        List<Upload> uploads = await this.uploadRepository.GetAllAsync(
            u => u.AppUserId == user.Id,
            cancellationToken: cancellationToken
        );

        return this.mapper.Map<IEnumerable<UploadDto>>(uploads);
    }

    public async Task<IEnumerable<UploadDto>> GetFavouritesAsync(
        CancellationToken cancellationToken = default
    )
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        int totalUploads = await this.uploadRepository.CountAsync(u =>
            u.AppUserId == user.Id && u.IsFavourited
        );

        List<Upload> uploads = await this.uploadRepository.GetAllAsync(
            u => u.AppUserId == user.Id && u.IsFavourited,
            cancellationToken: cancellationToken
        );

        return this.mapper.Map<IEnumerable<UploadDto>>(uploads);
    }

    public async Task<UploadDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        Upload upload = await this.GetUserUpload(id);

        return this.mapper.Map<UploadDto>(upload);
    }

    public async Task<UploadDto> FavouriteUpload(Guid id)
    {
        Upload upload = await this.GetUserUpload(id);

        upload.IsFavourited = !upload.IsFavourited;

        return this.mapper.Map<UploadDto>(await this.uploadRepository.UpdateAsync(upload));
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
