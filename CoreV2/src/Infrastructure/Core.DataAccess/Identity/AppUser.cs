using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.DataAccess.Identity;

public class AppUser : IdentityUser
{
    public IEnumerable<Upload> Uploads { get; } = [];
}
