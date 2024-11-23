namespace Core.Application.Models.User;

public record LoginUserDto
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

public record LoginUserResponseDto
{
    public required string Token { get; set; }
}
