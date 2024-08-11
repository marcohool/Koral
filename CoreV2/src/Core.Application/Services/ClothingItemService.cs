using AutoMapper;
using Core.Application.Dtos.ClothingItem;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Application.Services;

public class ClothingItemService(
    IMapper mapper,
    IClothingItemRepository clothingItemRepository,
    IImageStorageService imageStorageService
) : IClothingItemService
{
    private readonly IMapper mapper = mapper;
    private readonly IClothingItemRepository clothingItemRepository = clothingItemRepository;
    private readonly IImageStorageService imageStorageService = imageStorageService;

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

        this.clothingItemRepository.AddAsync(clothingItem);

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
        List<ClothingItem> clothingItems = await this.clothingItemRepository.GetAllAsync();

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
}
