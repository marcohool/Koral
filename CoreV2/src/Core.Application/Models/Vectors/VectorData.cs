namespace Core.Application.Models.Vectors;

public record VectorData<T>
{
    public required float[] Vector { get; set; }

    public required T Entity { get; set; }
}
