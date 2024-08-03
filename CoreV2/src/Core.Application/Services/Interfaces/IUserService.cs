using Core.Application.Dtos.User;

namespace Core.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> CreateAsync(CreateUserDto createUserDto);
}
