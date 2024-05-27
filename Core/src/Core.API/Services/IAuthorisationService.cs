using Core.API.Dto.Account;
using Core.API.Models;

namespace Core.API.Services;

/// <summary>
/// Interface for the <see cref="IAuthorisationService"/> class.
/// </summary>
public interface IAuthorisationService
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerRequest">The user registration details.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<RegisterResponse> RegisterUser(RegisterRequest registerRequest);

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="loginRequest">The user login details</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<AuthResponse> LoginUser(LoginRequest loginRequest);
}
