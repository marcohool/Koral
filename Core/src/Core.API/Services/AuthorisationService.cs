using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.API.Dto.Account;
using Core.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Core.API.Services;

/// <summary>
/// The <see cref="IAuthorisationService"/> interface.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AccountService"/> class.
/// </remarks>
/// <param name="userManager">Instance of user manager.</param>
/// <param name="signinManager">Instance of signin manager.</param>
/// <param name="configuration">Instance of <see cref="IConfiguration"/>.</param>
public class AccountService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signinManager,
    IConfiguration configuration
) : IAuthorisationService
{
    private readonly UserManager<AppUser> userManager = userManager;
    private readonly SignInManager<AppUser> signinManager = signinManager;
    private readonly IConfiguration configuration = configuration;

    /// <inheritdoc />
    public async Task RegisterUser(RegisterRequest registerRequest)
    {
        AppUser createdUser =
            new() { UserName = registerRequest.Email, Email = registerRequest.Email };

        IdentityResult registerResult = await this.userManager.CreateAsync(
            createdUser,
            registerRequest.Password
        );

        if (!registerResult.Succeeded)
            throw new InvalidOperationException("An error occurred while registering the user");

        IdentityResult roleResult = await this.userManager.AddToRoleAsync(createdUser, "User");

        if (!roleResult.Succeeded)
            throw new InvalidOperationException("An error occurred while assigning the user role");
    }

    /// <inheritdoc />
    public async Task<AuthResponse> LoginUser(LoginRequest loginRequest)
    {
        AppUser? user = await this.userManager.FindByEmailAsync(loginRequest.Email);

        if (user is null)
            return new AuthResponse { Succeeded = false };

        Microsoft.AspNetCore.Identity.SignInResult signinResult =
            await this.signinManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);

        if (signinResult.Succeeded)
            return new AuthResponse { Succeeded = true, Token = this.CreateToken(user) };
        if (signinResult.IsNotAllowed)
            return new AuthResponse { IsNotAllowed = true };
        if (signinResult.IsLockedOut)
            return new AuthResponse { IsLockedOut = true };

        return new AuthResponse { Succeeded = false };
    }

    /// <inheritdoc />
    private string CreateToken(AppUser user)
    {
        if (user.Email is null)
            throw new InvalidOperationException("User email is required");

        List<Claim> claims =
        [
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

        string signingKey =
            this.configuration["JWT:SigningKey"]
            ?? throw new ArgumentException("JWT signing key not configured.");

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(signingKey));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        SecurityTokenDescriptor tokenDescriptor =
            new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials,
                Issuer = this.configuration["JWT:Issuer"],
                Audience = this.configuration["JWT:Audience"],
            };

        JwtSecurityTokenHandler tokenHandler = new();

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
