namespace Core.API.Dto.Account;

/// <summary>
/// The <see cref="RegisterResponse"/> class.
/// </summary>
public class RegisterResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the operation succeeded.
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is not allowed.
    /// </summary>
    public IEnumerable<string> Errors { get; set; }
}
