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
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> userManager = userManager;

    public Task<string> GetClaim(string key)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        ClaimsPrincipal? user = this.httpContextAccessor.HttpContext?.User;

        return user is null ? null : await this.userManager.GetUserAsync(user);
    }
}
