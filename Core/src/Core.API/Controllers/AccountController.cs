using Core.API.Dto.Account;
using Core.API.Models;
using Core.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

/// <summary>
/// The <see cref="AccountController"/> class.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AccountController"/> class.
/// </remarks>
/// <param name="accountService">Instance of <see cref="IAuthorisationService"/>.</param>
[Route("[controller]")]
[ApiController]
public class AccountController(IAuthorisationService accountService) : ControllerBase
{
    private readonly IAuthorisationService accountService = accountService;

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerRequest">Instance of <see cref="RegisterRequest"/>.</param>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
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
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!this.ModelState.IsValid)
            return this.BadRequest(
                this.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            );

        try
        {
            AuthResponse authResponse = await this.accountService.LoginUser(loginRequest);

            return authResponse switch
            {
                { Succeeded: true } => this.Ok(authResponse.Token),
                { Succeeded: false } => this.BadRequest("Invalid username/password"),
                { IsNotAllowed: true } => this.BadRequest("User is not allowed"),
                { IsLockedOut: true } => this.BadRequest("User is locked out"),
                _ => throw new InvalidOperationException("Uncaught authentiation response"),
            };
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, ex.Message);
        }
    }
}
