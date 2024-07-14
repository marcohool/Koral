using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities;

public class AppUser : IdentityUser
{
    public IEnumerable<Upload> Uploads { get; } = [];
}
