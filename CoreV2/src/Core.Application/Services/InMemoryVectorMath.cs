using Core.Application.Models.Vectors;
using Core.Application.Services.Interfaces;

namespace Core.Application.Services;

public class InMemoryVectorMath : IVectorMath
{
    private const float FloatTolerance = 1e-6f;

    // TODO: Make async
    public List<SearchResult<T>> ComputeCosignSimilarity<T>(
        float[] queryVector,
        List<VectorData<T>> searchItems,
        float threshold
    )
    {
        List<SearchResult<T>> results = [];

        Parallel.ForEach(
            searchItems,
            vectorData =>
            {
                float similarity = CosineSimilarity(vectorData.Vector, queryVector);

                if (similarity >= threshold)
                {
                    results.Add(
                        new SearchResult<T> { Result = vectorData.Entity, Similarity = similarity }
                    );
                }
            }
        );

        return results.OrderByDescending(r => r.Similarity).ToList();
    }

    private static float CosineSimilarity(float[] vector1, float[] vector2)
    {
        float dotProduct = 0f;
        float magnitudeA = 0f;
        float magnitudeB = 0f;

        for (int i = 0; i < vector1.Length; i++)
        {
            dotProduct += vector1[i] * vector2[i];
            magnitudeA += vector1[i] * vector1[i];
            magnitudeB += vector2[i] * vector2[i];
        }

        if (Math.Abs(magnitudeA) < FloatTolerance || Math.Abs(magnitudeB) < FloatTolerance)
        {
            return 0;
        }

        return dotProduct / ((float)Math.Sqrt(magnitudeA) * (float)Math.Sqrt(magnitudeB));
    }

    private static float EuclideanDistance(float[] vector1, float[] vector2)
    {
        float sum = 0f;
        for (int i = 0; i < vector1.Length; i++)
        {
            float diff = vector1[i] - vector2[i];
            sum += diff * diff;
        }
        return (float)Math.Sqrt(sum);
    }
}
