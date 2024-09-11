namespace Core.Domain.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException() { }

    public ResourceNotFoundException(Type type)
        : base($"{type} is missing") { }
}
