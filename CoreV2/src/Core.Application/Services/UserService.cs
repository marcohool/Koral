using AutoMapper;
using Core.Application.Dtos.User;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Services;

public class UserService(IMapper mapper, UserManager<ApplicationUser> userManager) : IUserService
{
    private readonly IMapper mapper = mapper;
    private readonly UserManager<ApplicationUser> userManager = userManager;

    public async Task<UserResponseDto> CreateAsync(CreateUserDto createUserDto)
    {
        ApplicationUser user = mapper.Map<ApplicationUser>(createUserDto);

        IdentityResult result = await userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
        {
            throw new BadRequestException(result.Errors.Select(e => e.Description));
        }

        // To-do: Set up email confirmation

        return new UserResponseDto { Id = Guid.Parse(user.Id), };
    }
}
