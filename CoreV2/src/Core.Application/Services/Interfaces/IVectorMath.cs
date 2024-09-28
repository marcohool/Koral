using Core.Application.Models.Vectors;

namespace Core.Application.Services.Interfaces;

public interface IVectorMath
{
    List<SearchResult<T>> ComputeCosignSimilarity<T>(
        float[] queryVector,
        List<VectorData<T>> searchItems,
        float threshold
    );
}
