using Core.Application.Models.User;

namespace Core.Application.Services.Interfaces;

public interface IUserService
{
    Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);

    Task<LoginUserResponseDto> LoginAsync(LoginUserDto loginUserDto);
}
