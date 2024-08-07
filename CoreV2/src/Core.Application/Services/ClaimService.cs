using System.Security.Claims;
using Core.Application.Services.Interfaces;
using Core.DataAccess.Identity;
using Core.DataAccess.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services;

public class ClaimService(IHttpContextAccessor httpContextAccessor, DatabaseContext context)
    : IClaimService
{
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    private readonly DatabaseContext context = context;

    public Task<string> GetClaim(string key)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        string? loggedInUserId = this.httpContextAccessor.HttpContext?.User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );

        return await this
            .context.Users.AsNoTracking()
            .FirstOrDefaultAsync(au => au.Id == loggedInUserId);
    }
}
