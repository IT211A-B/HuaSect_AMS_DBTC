using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace HuaSect_AMS_DBTCtests;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "TestAuth";
    public ClaimsPrincipal? TestUser { get; set; }

    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (TestUser == null)
            return Task.FromResult(AuthenticateResult.Fail("No test user configured"));

        var ticket = new AuthenticationTicket(TestUser, SchemeName);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}