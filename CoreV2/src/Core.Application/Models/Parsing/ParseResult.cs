namespace Core.Application.Models.Parsing;

public class ParseResult<T>
{
    public IEnumerable<T> Successes { get; set; } = [];

    public string? ErrorMessage { get; set; }
}
