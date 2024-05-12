using Core.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;

    public AccountController(UserManager<AppUser> userManager)
    {
        this.userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        try
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(
                    this.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                );

            AppUser createdUser =
                new() { UserName = registerRequest.Email, Email = registerRequest.Email };

            IdentityResult registerResult = await this.userManager.CreateAsync(
                createdUser,
                registerRequest.Password
            );

            if (registerResult.Succeeded)
            {
                IdentityResult roleResult = await this.userManager.AddToRoleAsync(
                    createdUser,
                    "User"
                );

                if (roleResult.Succeeded)
                    return this.Ok("User created");

                return this.BadRequest(roleResult.Errors);
            }

            return this.BadRequest(registerResult.Errors);
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }
}
