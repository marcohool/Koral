namespace Core.API.Dto.Account;

/// <summary>
/// The <see cref="AuthResponse"/> class.
/// </summary>
public record AuthResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the operation succeeded.
    /// </summary>
    public bool? Succeeded { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is not allowed.
    /// </summary>
    public bool? IsNotAllowed { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is locked out.
    /// </summary>
    public bool? IsLockedOut { get; set; }

    /// <summary>
    /// Gets or sets the JWT token.
    /// </summary>
    public string? Token { get; set; }
}
