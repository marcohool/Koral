using Core.Application.Dtos.User;
using Core.Application.Exceptions;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

public class UsersController(IUserService userService) : ApiController
{
    private readonly IUserService userService = userService;

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(CreateUserDto createUserDto)
    {
        try
        {
            UserResponseDto user = await userService.CreateAsync(createUserDto);
            return Ok(user);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.ErrorMessages);
        }
    }
}
