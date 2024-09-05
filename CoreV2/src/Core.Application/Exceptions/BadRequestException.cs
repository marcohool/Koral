namespace Core.Application.Exceptions;

public class BadRequestException : Exception
{
    public IEnumerable<string>? ErrorMessages { get; }

    public BadRequestException(string message)
        : base(message) { }

    public BadRequestException(IEnumerable<string> messages)
        : base(string.Join("; ", messages))
    {
        this.ErrorMessages = messages;
    }
}
