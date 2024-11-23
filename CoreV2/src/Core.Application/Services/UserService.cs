using AutoMapper;
using Core.Application.Configuration;
using Core.Application.Exceptions;
using Core.Application.Helpers;
using Core.Application.Models.User;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Core.Application.Services;

public class UserService(
    IMapper mapper,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IOptionsMonitor<JwtOptions> jwtOptions
) : IUserService
{
    private readonly IMapper mapper = mapper;
    private readonly UserManager<ApplicationUser> userManager = userManager;
    private readonly SignInManager<ApplicationUser> signInManager = signInManager;

    public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto)
    {
        ApplicationUser user = this.mapper.Map<ApplicationUser>(createUserDto);

        IdentityResult result = await this.userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
        {
            throw new BadRequestException(result.Errors.Select(e => e.Description));
        }

        // To-do: Set up email confirmation

        return new CreateUserResponseDto
        {
            Id = Guid.Parse(user.Id),
            Token = JwtHelper.GenerateToken(user, jwtOptions.CurrentValue)
        };
    }

    public async Task<LoginUserResponseDto> LoginAsync(LoginUserDto loginUserDto)
    {
        const string errorMessage = "Invalid email or password";

        ApplicationUser? user = await this.userManager.FindByEmailAsync(loginUserDto.Email);

        if (user is null)
        {
            throw new BadRequestException(errorMessage);
        }

        SignInResult signInResult = await this.signInManager.CheckPasswordSignInAsync(
            user,
            loginUserDto.Password,
            false
        );

        if (signInResult.Succeeded)
        {
            return new LoginUserResponseDto()
            {
                Token = JwtHelper.GenerateToken(user, jwtOptions.CurrentValue)
            };
        }

        if (signInResult.IsLockedOut)
        {
            throw new BadRequestException("Account is locked out");
        }

        throw new BadRequestException(errorMessage);
    }
}
