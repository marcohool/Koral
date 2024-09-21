namespace Core.Domain.Exceptions;

public class EmbeddingGenerationException : Exception
{
    public EmbeddingGenerationException() { }

    public EmbeddingGenerationException(string message)
        : base(message) { }
}
