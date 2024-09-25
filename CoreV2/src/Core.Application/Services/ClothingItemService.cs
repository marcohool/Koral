using AutoMapper;
using Core.Application.Dtos.ClothingItem;
using Core.Application.Exceptions;
using Core.Application.Models.Parsing;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Application.Services;

public class ClothingItemService(
    IMapper mapper,
    IClothingItemRepository clothingItemRepository,
    IImageStorageService imageStorageService,
    IClothingItemParser clothingItemParser,
    IServiceProvider serviceProvider
) : IClothingItemService
{
    private readonly IMapper mapper = mapper;
    private readonly IClothingItemRepository clothingItemRepository = clothingItemRepository;
    private readonly IImageStorageService imageStorageService = imageStorageService;
    private readonly IClothingItemParser clothingItemParser = clothingItemParser;
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task<ClothingItemResponseDto> CreateAsync(
        CreateClothingItemDto createClothingItemModel,
        CancellationToken cancellationToken = default
    )
    {
        ClothingItem clothingItem = this.mapper.Map<ClothingItem>(createClothingItemModel);

        clothingItem.ImageUrl = await this.imageStorageService.UploadImageAsync(
            createClothingItemModel.Image,
            cancellationToken
        );

        await this.clothingItemRepository.AddAsync(clothingItem);

        return this.mapper.Map<ClothingItemResponseDto>(clothingItem);
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ClothingItem? clothingItem = await this.clothingItemRepository.GetFirstAsync(ci =>
            ci.Id == id
        );

        if (clothingItem is null)
        {
            throw new NotFoundException($"Clothing item with id {id} not found");
        }

        IDbContextTransaction transaction =
            await this.clothingItemRepository.BeginTransactionAsync();

        Guid deletedId = await this.clothingItemRepository.DeleteAsync(clothingItem);

        if (clothingItem.ImageUrl is not null)
        {
            await this.imageStorageService.DeleteImageAsync(
                clothingItem.ImageUrl,
                cancellationToken
            );
        }
        else
        {
            // To-do: Else log that image url is missing
        }

        await transaction.CommitAsync(cancellationToken);

        return deletedId;
    }

    public async Task<IEnumerable<ClothingItemResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        List<ClothingItem> clothingItems = await this.clothingItemRepository.GetAllAsync(
            cancellationToken: cancellationToken
        );

        return this.mapper.Map<IEnumerable<ClothingItemResponseDto>>(clothingItems);
    }

    public async Task<ClothingItemResponseDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        ClothingItem? clothingItem = await this.clothingItemRepository.GetFirstAsync(ci =>
            ci.Id == id
        );

        if (clothingItem is null)
        {
            throw new NotFoundException($"Clothing item with id {id} not found");
        }

        return this.mapper.Map<ClothingItemResponseDto>(clothingItem);
    }

    public async Task ImportClothingItems(IFormFile file)
    {
        ParseResult<ClothingItemImport> parseResult = await this.clothingItemParser.Parse(file);

        if (parseResult.ErrorMessage is not null)
        {
            throw new JsonSerializationException(parseResult.ErrorMessage);
        }

        IDbContextTransaction transaction =
            await this.clothingItemRepository.BeginTransactionAsync();

        await Parallel.ForEachAsync(
            parseResult.Successes,
            async (clothingItemImport, CancellationToken) =>
            {
                // To-do: Potentially upload image url to image storage service
                using IServiceScope scope = this.serviceProvider.CreateScope();

                IClothingItemRepository scopedRepository =
                    scope.ServiceProvider.GetRequiredService<IClothingItemRepository>();
                IMapper scopedMapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                ClothingItem clothingItem = scopedMapper.Map<ClothingItem>(clothingItemImport);

                await scopedRepository.AddAsync(clothingItem);
            }
        );

        await this.clothingItemRepository.CommitTransactionAsync(transaction);
    }

    private async Task UpsertCollectionAsync(
        IEnumerable<ClothingItemImport> clothingItemImports,
        CancellationToken cancellationToken = default
    )
    {
        foreach (ClothingItemImport clothingItemImport in clothingItemImports)
        {
            ClothingItem? existingClothingItem = await this.clothingItemRepository.GetFirstAsync(
                ci =>
                    ci.Name == clothingItemImport.Name
                        && ci.Brand == clothingItemImport.Brand
                        && ci.SourceRegion == clothingItemImport.SourceRegion
                    || ci.SourceUrl.Equals(clothingItemImport.SourceUrl)
            );

            if (existingClothingItem is not null)
            {
                string existingImageUrl = existingClothingItem.ImageUrl;

                existingClothingItem = this.mapper.Map<ClothingItem>(clothingItemImport);
                existingClothingItem.ImageUrl = existingImageUrl;

                await this.clothingItemRepository.UpdateAsync(existingClothingItem);

                continue;
            }

            ClothingItem clothingItem = this.mapper.Map<ClothingItem>(clothingItemImport);

            // Upload image url

            await this.clothingItemRepository.AddAsync(clothingItem);
        }
    }
}
