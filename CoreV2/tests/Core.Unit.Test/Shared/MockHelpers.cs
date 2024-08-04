using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Core.UnitTest.Shared;

public class MockHelpers
{
    public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> userList)
        where TUser : class
    {
        Mock<IUserStore<TUser>> store = new();
        Mock<IPasswordHasher<TUser>> passwordHasher = new();

        Mock<UserManager<TUser>> userManager =
            new(
                store.Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<TUser>>().Object,
                Array.Empty<IUserValidator<TUser>>(),
                Array.Empty<IPasswordValidator<TUser>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object
            );

        userManager.Object.UserValidators.Add(new UserValidator<TUser>());
        userManager.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        return userManager;
    }

    public static Mock<SignInManager<TUser>> MockSignInManager<TUser>(
        Mock<UserManager<TUser>> userManager
    )
        where TUser : class
    {
        Mock<SignInManager<TUser>> signInManager =
            new(
                userManager.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<TUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<TUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<TUser>>().Object
            );

        return signInManager;
    }
}
