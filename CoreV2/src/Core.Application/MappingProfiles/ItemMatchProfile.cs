using AutoMapper;
using Core.Application.Models.ItemMatch;
using Core.Domain.Entities;

namespace Core.Application.MappingProfiles;

public class ItemMatchProfile : Profile
{
    public ItemMatchProfile()
    {
        this.CreateMap<ItemMatch, ItemMatchResponseDto>()
            .IncludeMembers(src => src.ClothingItem)
            .ForMember(
                dest => dest.OverallSimilarity,
                opt =>
                    opt.MapFrom(src =>
                        this.CalculateSimilarity(src.EmbeddingSimilarity, src.ColourSimilarity)
                    )
            );
    }

    private float CalculateSimilarity(float embeddingSimilarity, double colourSimilarity)
    {
        //return (float)((embeddingSimilarity + colourSimilarity) / 2.0);
        return embeddingSimilarity;
    }
}
