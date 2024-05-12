using Core.API.Dto.Account;
using Core.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Services;

/// <summary>
/// The <see cref="IAccountService"/> interface.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AccountService"/> class.
/// </remarks>
/// <param name="userManager">Instance of user manager.</param>
public class AccountService(UserManager<AppUser> userManager) : IAccountService
{
    private readonly UserManager<AppUser> userManager = userManager;

    /// <inheritdoc />
    public async Task RegisterUser(RegisterRequest registerRequest)
    {
        AppUser createdUser =
            new() { UserName = registerRequest.Email, Email = registerRequest.Email };

        try
        {
            IdentityResult registerResult = await this.userManager.CreateAsync(
                createdUser,
                registerRequest.Password
            );

            if (!registerResult.Succeeded)
                throw new InvalidOperationException("An error occurred while registering the user");

            IdentityResult roleResult = await this.userManager.AddToRoleAsync(createdUser, "User");

            if (!roleResult.Succeeded)
                throw new InvalidOperationException(
                    "An error occurred while assigning the user role"
                );
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while registering the user", ex);
        }
    }
}
