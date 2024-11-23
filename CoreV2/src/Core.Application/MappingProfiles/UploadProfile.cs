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
                                .Select(ci => ci.ClothingItem)
                                .GroupBy(ci => ci.Category)
                                .Select(group => new CategorisedItemMatches
                                {
                                    Category = group.Key,
                                    ItemMatches = group
                                        .SelectMany(im =>
                                            context.Mapper.Map<List<ItemMatchResponseDto>>(
                                                im.ItemMatches
                                            )
                                        )
                                        .ToList()
                                })
                                .OrderByDescending(cim => cim.Category)
                    )
            );
    }
}
