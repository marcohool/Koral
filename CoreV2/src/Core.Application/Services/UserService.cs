using Core.Application.Dtos.User;
using Core.Application.Services.Interfaces;

namespace Core.Application.Services;

public class UserService : IUserService
{
    public Task<UserResponseDto> CreateAsync(CreateUserDto createUserDto)
    {
        throw new NotImplementedException();
    }
}
