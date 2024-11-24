using System.Threading;
using AutoMapper;
using Core.Application.APIs.KoralMatch;
using Core.Application.APIs.KoralMatch.Models;
using Core.Application.Exceptions;
using Core.Application.Models.ItemMatch;
using Core.Application.Models.Upload;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Identity;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        string imageUrl = await this.imageStorageService.UploadImageAsync(image, cancellationToken);

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
        uploadDto.MatchedClothingItems = this.mapper.Map<List<CategorisedItemMatches>>(allMatches);

        return uploadDto;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        Upload? upload = await this
            .uploadRepository.GetAll(u => u.Id == id && u.AppUserId == user.Id, cancellationToken)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (upload is null)
        {
            throw new NotFoundException($"Upload with id {id} not found");
        }

        IDbContextTransaction transaction = await this.uploadRepository.BeginTransactionAsync();

        await this.uploadRepository.DeleteAsync(upload);
        await this.imageStorageService.DeleteImageAsync(upload.ImageUrl, cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task<IEnumerable<UploadDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        List<Upload> uploads = await this
            .uploadRepository.GetAll(cancellationToken: cancellationToken)
            .Where(u => u.AppUserId == user.Id)
            .Include(u => u.UploadItems)
            .ThenInclude(ui => ui.ItemMatches)
            .ThenInclude(im => im.ClothingItem)
            .ToListAsync(cancellationToken: cancellationToken);

        return this.mapper.Map<IEnumerable<UploadDto>>(uploads);
    }

    public async Task<IEnumerable<UploadDto>> GetFavouritesAsync(
        CancellationToken cancellationToken = default
    )
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        List<Upload> uploads = await this
            .uploadRepository.GetAll(cancellationToken: cancellationToken)
            .Where(u => u.AppUserId == user.Id && u.IsFavourited)
            .ToListAsync(cancellationToken: cancellationToken);

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

    public async Task FavouriteUpload(Guid id)
    {
        Upload upload = await this.GetUserUpload(id);

        upload.IsFavourited = !upload.IsFavourited;
    }

    private async Task<Upload> GetUserUpload(Guid id)
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        Upload? upload = await this
            .uploadRepository.GetAll()
            .Where(u => u.Id == id && u.AppUserId == user.Id)
            .Include(u => u.UploadItems)
            .ThenInclude(ui => ui.ItemMatches)
            .ThenInclude(im => im.ClothingItem)
            .FirstOrDefaultAsync();

        if (upload is null)
        {
            throw new NotFoundException($"Upload with id {id} not found");
        }

        return upload;
    }
}
