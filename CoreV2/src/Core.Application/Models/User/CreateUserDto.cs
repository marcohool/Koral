namespace Core.Application.Models.User;

public record CreateUserDto
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

public record CreateUserResponseDto : BaseResponseDto
{
    public required string Token { get; set; }
}
