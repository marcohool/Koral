using AutoMapper;
using Core.Application.Dtos.ClothingItem;
using Core.Domain.Entities;

namespace Core.Application.MappingProfiles;

public class ClothingItemProfile : Profile
{
    public ClothingItemProfile()
    {
        this.CreateMap<CreateClothingItemDto, ClothingItem>();
        this.CreateMap<ClothingItem, ClothingItemResponseDto>();
    }
}
