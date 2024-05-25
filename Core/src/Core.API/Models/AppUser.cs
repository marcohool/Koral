using Microsoft.AspNetCore.Identity;

namespace Core.API.Models;

/// <summary>
/// The <see cref="AppUser"/> class.
/// </summary>
public class AppUser : IdentityUser
{
    /// <summary>
    /// The <see cref="ImageUpload"/> property representing images uploaded by this user.
    /// </summary>
    public ICollection<ImageUpload> ImageUploads { get; } = [];
}
