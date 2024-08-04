using System.Security.Claims;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Services;

public class ClaimService(
    IHttpContextAccessor httpContextAccessor,
    UserManager<ApplicationUser> userManager
) : IClaimService
{
    public Task<string> GetClaim(string key)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;

        return user is null ? null : await userManager.GetUserAsync(user);
    }
}
