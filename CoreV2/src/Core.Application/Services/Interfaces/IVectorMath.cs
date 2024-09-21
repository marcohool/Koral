using Core.Application.Models.Vectors;

namespace Core.Application.Services.Interfaces;

public interface IVectorMath
{
    List<SearchResult> ComputeCosignSimilarity(
        float[] queryVector,
        List<VectorData> searchItems,
        float threshold,
        int topN
    );
}
