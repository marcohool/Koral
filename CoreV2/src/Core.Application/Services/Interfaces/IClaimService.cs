using Core.DataAccess.Identity;

namespace Core.Application.Services.Interfaces;

public interface IClaimService
{
    Task<string> GetClaim(string key);

    Task<ApplicationUser> GetCurrentUserAsync();
}
