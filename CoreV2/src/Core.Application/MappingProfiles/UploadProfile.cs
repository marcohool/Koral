using AutoMapper;
using Core.Application.Models.ItemMatch;
using Core.Application.Models.Upload;
using Core.Domain.Entities;

namespace Core.Application.MappingProfiles;

public class UploadProfile : Profile
{
    public UploadProfile()
    {
        this.CreateMap<Upload, UploadDto>()
            .ForMember(
                dest => dest.MatchedClothingItems,
                opt =>
                    opt.MapFrom(
                        (src, _, _, context) =>
                            src
                                .UploadItems.Where(ui => ui.ItemMatches.Count != 0)
                                .SelectMany(ui => ui.ItemMatches)
                                .GroupBy(ci => ci.ClothingItem.Category)
                                .Select(group => new CategorisedItemMatches
                                {
                                    Category = group.Key,
                                    ItemMatches = group
                                        .DistinctBy(ci => ci.ClothingItemId)
                                        .Select(ci => context.Mapper.Map<ItemMatchResponseDto>(ci))
                                        .ToList()
                                })
                    )
            );
    }
}
