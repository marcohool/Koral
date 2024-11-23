using AutoMapper;
using Core.Application.Exceptions;
using Core.Application.Models.ClothingItem;
using Core.Application.Models.Parsing;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ClothingItem? clothingItem = await this
            .clothingItemRepository.GetAll(cancellationToken: cancellationToken)
            .Where(ci => ci.Id == id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (clothingItem is null)
        {
            throw new NotFoundException($"Clothing item with id {id} not found");
        }

        IDbContextTransaction transaction =
            await this.clothingItemRepository.BeginTransactionAsync();

        await this.clothingItemRepository.DeleteAsync(clothingItem);

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
    }

    public async Task<IEnumerable<ClothingItemResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        List<ClothingItem> clothingItems = await this
            .clothingItemRepository.GetAll(cancellationToken: cancellationToken)
            .ToListAsync(cancellationToken: cancellationToken);

        return this.mapper.Map<IEnumerable<ClothingItemResponseDto>>(clothingItems);
    }

    public async Task<ClothingItemResponseDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        ClothingItem? clothingItem = await this
            .clothingItemRepository.GetAll(cancellationToken: cancellationToken)
            .Where(ci => ci.Id == id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

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
}
