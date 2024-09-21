namespace Core.Application.Models.Vectors;

public record VectorData
{
    public required float[] Vector { get; set; }

    public required Guid Id { get; set; }
}
