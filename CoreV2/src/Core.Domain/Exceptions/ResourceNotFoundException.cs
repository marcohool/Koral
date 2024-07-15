namespace Core.Domain.Exceptions;

[Serializable]
public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException() { }

    public ResourceNotFoundException(Type type) : base($"{type} is missing") { }
}