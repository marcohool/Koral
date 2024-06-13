using System.ComponentModel.DataAnnotations;

namespace Core.API.Dto.Account;

/// <summary>
/// Represents a request to register a new user.
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// The user's email address which acts as a user name.
    /// </summary>
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    /// <summary>
    /// The user's password.
    /// </summary>
    [Required]
    [MinLength(6)]
    public required string Password { get; init; }
}
