using AutoMapper;
using Core.Application.Dtos.ClothingItem;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Exceptions;

namespace Core.Application.Services;

public class ClothingItemService(
    IMapper mapper,
    IClothingItemRepository clothingItemRepository,
    IImageStorageService imageStorageService
) : IClothingItemService
{
    public async Task<ClothingItemResponseDto> CreateAsync(
        CreateClothingItemDto createClothingItemModel,
        CancellationToken cancellationToken = default
    )
    {
        ClothingItem clothingItem = mapper.Map<ClothingItem>(createClothingItemModel);

        clothingItem.ImageUrl = await imageStorageService.UploadImageAsync(
            createClothingItemModel.Image,
            cancellationToken
        );

        ClothingItem createdClothingItem = await clothingItemRepository.AddAsync(clothingItem);

        return mapper.Map<ClothingItemResponseDto>(createdClothingItem);
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ClothingItem? clothingItem = await clothingItemRepository.GetFirstAsync(ci => ci.Id == id);

        if (clothingItem is null)
        {
            throw new NotFoundException($"Clothing item with id {id} not found");
        }

        return clothingItemRepository.DeleteAsync(clothingItem).Result;
    }

    public async Task<IEnumerable<ClothingItemResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        List<ClothingItem> clothingItems = await clothingItemRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ClothingItemResponseDto>>(clothingItems);
    }

    public async Task<ClothingItemResponseDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            ClothingItem? clothingItem = await clothingItemRepository.GetFirstAsync(ci =>
                ci.Id == id
            );

            return mapper.Map<ClothingItemResponseDto>(clothingItem);
        }
        catch (ResourceNotFoundException)
        {
            throw new NotFoundException($"Clothing item with id {id} not found");
        }
    }
}
