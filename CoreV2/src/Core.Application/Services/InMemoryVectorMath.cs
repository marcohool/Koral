using Core.Application.Models.Vectors;
using Core.Application.Services.Interfaces;

namespace Core.Application.Services;

public class InMemoryVectorMath : IVectorMath
{
    const float FloatTolerance = 1e-6f;

    public List<SearchResult> ComputeCosignSimilarity(
        float[] queryVector,
        List<VectorData> searchItems,
        float threshold,
        int topN
    )
    {
        List<SearchResult> results = [];

        Parallel.ForEach(
            searchItems,
            vectorData =>
            {
                float similarity = CosineSimilarity(vectorData.Vector, queryVector);

                if (similarity >= threshold)
                {
                    results.Add(new SearchResult { Id = vectorData.Id, Similarity = similarity });
                }
            }
        );

        return results.OrderByDescending(r => r.Similarity).Take(topN).ToList();
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
