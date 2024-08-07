using Core.Application.Dtos.User;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

public class UsersController(IUserService userService) : ApiController
{
    private readonly IUserService userService = userService;

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(CreateUserDto createUserDto)
    {
        try
        {
            return this.Ok(await this.userService.CreateAsync(createUserDto));
        }
        catch (BadRequestException ex)
        {
            return this.BadRequest(ex.ErrorMessages);
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginUserDto loginUserDto)
    {
        try
        {
            return this.Ok(await this.userService.LoginAsync(loginUserDto));
        }
        catch (BadRequestException ex)
        {
            return this.BadRequest(ex.Message);
        }
    }
}
