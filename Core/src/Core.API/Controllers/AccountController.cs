﻿using Core.API.Dto.Account;
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
            RegisterResponse registerResponse = await this.accountService.RegisterUser(
                registerRequest
            );

            if (!registerResponse.Succeeded)
                return this.BadRequest(registerResponse.Errors);

            return this.Ok("User reigstered successfully");
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="loginRequest">Instance of <see cref="LoginRequest"/>.</param>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    /// <exception cref="InvalidOperationException">Uncaught authentication error.</exception>
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
