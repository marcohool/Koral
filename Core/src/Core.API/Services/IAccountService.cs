using Core.API.Dto.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Services;

/// <summary>
/// Interface for the <see cref="IAccountService"/> class.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerRequest">The user registration details.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task RegisterUser(RegisterRequest registerRequest);
}
