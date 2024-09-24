using System.Collections.Generic;
using AutoMapper;
using Core.Application.APIs.KoralMatch;
using Core.Application.APIs.KoralMatch.Models;
using Core.Application.Dtos;
using Core.Application.Dtos.Upload;
using Core.Application.Exceptions;
using Core.Application.Models.Vectors;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Identity;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Application.Services;

public class UploadService(
    IMapper mapper,
    IImageStorageService imageStorageService,
    IUploadRepository uploadRepository,
    IClothingItemRepository clothingItemRepository,
    IClaimService claimService,
    IKoralMatchApi koralMatchApi,
    IVectorMath vectorMath,
    IItemMatchRepository uploadMatchesRepository,
    IUploadItemRepository uploadItemRepository
) : IUploadService
{
    private readonly IMapper mapper = mapper;
    private readonly IImageStorageService imageStorageService = imageStorageService;
    private readonly IUploadRepository uploadRepository = uploadRepository;
    private readonly IClothingItemRepository clothingItemRepository = clothingItemRepository;
    private readonly IClaimService claimService = claimService;
    private readonly IKoralMatchApi koralMatchApi = koralMatchApi;
    private readonly IVectorMath vectorMath = vectorMath;
    private readonly IItemMatchRepository uploadMatchesRepository = uploadMatchesRepository;
    private readonly IUploadItemRepository uploadItemRepository = uploadItemRepository;

    private readonly List<Gender> BaseGenders = [Gender.Unknown, Gender.Unisex];

    public async Task<UploadDto> CreateAsync(
        CreateUploadDto createClothingItemRequestModel,
        CancellationToken cancellationToken = default
    )
    {
        ApplicationUser user = await this.claimService.GetCurrentUserAsync();

        IFormFile image = createClothingItemRequestModel.Image;
        //string imageUrl = await this.imageStorageService.UploadImageAsync(image, cancellationToken);
        string imageUrl = "www.testing.com";

        UploadEmbedding uploadEmbedding = await this.koralMatchApi.GetUploadEmbedding(image);

        Upload upload =
            new()
            {
                Title = uploadEmbedding.Title,
                ImageUrl = imageUrl,
                AppUserId = user.Id
            };

        await this.uploadRepository.AddAsync(upload);

        foreach (ClothingItemEmbedding ciEmbedding in uploadEmbedding.ClothingItemEmbeddings ?? [])
        {
            UploadItem uploadItem =
                new()
                {
                    Description = ciEmbedding.Description,
                    Embedding = ciEmbedding.EmbeddingVector,
                    HexColours = ciEmbedding.Colours,
                    Upload = upload
                };
            await this.uploadItemRepository.AddAsync(uploadItem);

            this.BaseGenders.AddRange(
                this.BaseGenders.Contains(ciEmbedding.Gender)
                    ? [Gender.Male, Gender.Female]
                    : [ciEmbedding.Gender]
            );

            List<ClothingItem> clothingItemsToSearch =
                await this.clothingItemRepository.GetAllAsync(
                    ci =>
                        ci.Category == ciEmbedding.Category && this.BaseGenders.Contains(ci.Gender),
                    cancellationToken: cancellationToken
                );

            List<VectorData> vectors = clothingItemsToSearch
                .Select(ci => new VectorData { Vector = ci.EmbeddingVector, Id = ci.Id })
                .ToList();

            List<SearchResult> matches = this.vectorMath.ComputeCosignSimilarity(
                ciEmbedding.EmbeddingVector,
                vectors,
                0.5f,
                10
            );

            foreach (SearchResult searchResult in matches)
            {
                ClothingItem clothingItem = clothingItemsToSearch.First(ci =>
                    ci.Id == searchResult.Id
                );

                ItemMatch uploadMatch =
                    new()
                    {
                        EmbeddingSimilarity = searchResult.Similarity,
                        ColourSimilarity = 0f,
                        UploadItem = uploadItem,
                        ClothingItem = clothingItem,
                    };

                await this.uploadMatchesRepository.AddAsync(uploadMatch);
            }
        }

        return this.mapper.Map<UploadDto>(upload);
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

    public async Task<PaginatedResponse<UploadDto>> GetAllAsync(
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

        return new PaginatedResponse<UploadDto>(
            this.mapper.Map<IEnumerable<UploadDto>>(uploads),
            pageNumber,
            pageSize,
            totalUploads
        );
    }

    public async Task<PaginatedResponse<UploadDto>> GetFavouritesAsync(
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

        return new PaginatedResponse<UploadDto>(
            this.mapper.Map<IEnumerable<UploadDto>>(uploads),
            pageNumber,
            pageSize,
            totalUploads
        );
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
