using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using Core.Application.APIs.KoralMatch.Models;
using Core.Application.Models.Matching;
using Core.Application.Models.Vectors;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Application.Services;

public class MatchingService(
    IVectorMath vectorMathService,
    IClothingItemRepository clothingItemRepository
) : IMatchingService
{
    private readonly IVectorMath vectorMatchService = vectorMathService;
    private readonly IClothingItemRepository clothingItemRepository = clothingItemRepository;

    // TODO: Move to configuration
    private const double DeltaEThreshold = 60d;
    private const float CosineSimilarityThreshold = 0.5f;
    private const int TopN = 10;

    public async Task<IEnumerable<MatchResult>> GetMatches(
        ItemEmbedding itemEmbedding,
        CancellationToken cancellationToken
    )
    {
        List<ClothingItem> clothingItemsToSearch = await this.GetClothingItemsToSearch(
            itemEmbedding,
            cancellationToken
        );

        List<SearchResult<ClothingItem>> imageMatches =
            this.vectorMatchService.ComputeCosignSimilarity(
                itemEmbedding.EmbeddingVector,
                clothingItemsToSearch
                    .Select(ci => new VectorData<ClothingItem>
                    {
                        Vector = ci.EmbeddingVector,
                        Entity = ci
                    })
                    .ToList(),
                threshold: CosineSimilarityThreshold
            );

        List<MatchResult> matches = [];

        foreach (SearchResult<ClothingItem> match in imageMatches)
        {
            double colourDifference = ComputeColourDifference(itemEmbedding.Colour, match.Result);

            if (colourDifference < DeltaEThreshold)
            {
                matches.Add(
                    new MatchResult()
                    {
                        ClothingItem = match.Result,
                        EmbeddingSimilarity = match.Similarity,
                        ColourSimilarity = colourDifference
                    }
                );
            }
        }

        return matches.Take(TopN);
    }

    private static double ComputeColourDifference(string itemColour, ClothingItem matchedItem)
    {
        Lab itemLabColour = new Hex(itemColour).To<Lab>();
        double lowestDeltaE = 100;

        foreach (string matchedItemColour in matchedItem.Colours)
        {
            // TODO: Save ClothingItems as Lab colours not hex
            Lab matchedItemLabColour = new Hex(matchedItemColour).To<Lab>();

            lowestDeltaE = Math.Min(
                lowestDeltaE,
                itemLabColour.Compare(matchedItemLabColour, new Cie1976Comparison())
            );
        }

        return lowestDeltaE;
    }

    private async Task<List<ClothingItem>> GetClothingItemsToSearch(
        ItemEmbedding itemEmbedding,
        CancellationToken cancellationToken
    )
    {
        List<Gender> baseGenders = [Gender.Unknown, Gender.Unisex];

        baseGenders.AddRange(
            baseGenders.Contains(itemEmbedding.Gender)
                ? [Gender.Male, Gender.Female]
                : [itemEmbedding.Gender]
        );

        List<ClothingItem> clothingItemsToSearch = await this.clothingItemRepository.GetAllAsync(
            ci => ci.Category == itemEmbedding.Category && baseGenders.Contains(ci.Gender),
            cancellationToken: cancellationToken
        );

        return clothingItemsToSearch;
    }
}
