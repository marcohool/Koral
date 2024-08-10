using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Core.DataAccess.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.Integration.Test.Helpers;

public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string AuthenticationScheme = "Test";
    private readonly ApplicationUser authenticatedUser = DatabaseContextHelper.authenticatedUser;

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Email, this.authenticatedUser.Email!),
            new Claim(JwtRegisteredClaimNames.Sub, this.authenticatedUser.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, this.authenticatedUser.Id)
        ];

        ClaimsIdentity identity = new(claims, AuthenticationScheme);
        ClaimsPrincipal principal = new(identity);
        AuthenticationTicket ticket = new(principal, AuthenticationScheme);

        AuthenticateResult result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}
