using AutoMapper;
using Core.Application.Models.ClothingItem;
using Core.Application.Models.ItemMatch;
using Core.Domain.Entities;

namespace Core.Application.MappingProfiles;

public class ClothingItemProfile : Profile
{
    public ClothingItemProfile()
    {
        this.CreateMap<CreateClothingItemDto, ClothingItem>();
        this.CreateMap<ClothingItem, ClothingItemResponseDto>();
        this.CreateMap<ClothingItemImport, ClothingItem>();
        this.CreateMap<ClothingItem, ItemMatchResponseDto>();
    }
}
