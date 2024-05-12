using Core.API.Dto.Account;
using Core.API.Models;
using Core.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if (!this.ModelState.IsValid)
            return this.BadRequest(
                this.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            );

        try
        {
            await this.accountService.RegisterUser(registerRequest);

            return this.Ok("User created");
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        try
        {
            AppUser user = await this.userManager.FindByEmailAsync(loginRequest.Email);

            if (user is null)
                return this.BadRequest("User not found");

            bool passwordValid = await this.userManager.CheckPasswordAsync(
                user,
                loginRequest.Password
            );

            if (!passwordValid)
                return this.BadRequest("Invalid password");

            return this.Ok("User logged in");
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }
}
