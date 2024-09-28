using Core.Domain.Entities;

namespace Core.Application.Models.Vectors;

public class SearchResult<T>
{
    public required T Result { get; set; }

    public float Similarity { get; set; }
}
