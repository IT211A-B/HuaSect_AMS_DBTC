using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace HuaSect_AMS_DBTCtests;

[TestClass]
public class AuthorizationTests
{
    private static IntegrationTestFactory _factory;
    private static HttpClient _client;

    [ClassInitialize]
    public static async Task ClassInit(TestContext context)
    {
        _factory = new IntegrationTestFactory();
        await _factory.InitializeAsync();
    }

    [ClassCleanup]
    public static async Task ClassCleanup() => await _factory.DisposeAsync();

    private async Task<HttpClient> GetClientAsync(params Claim[] claims)
    {
        var client = _factory.CreateClient();
        var scheme = await _factory.Services
            .GetRequiredService<IAuthenticationSchemeProvider>()
            .GetSchemeAsync(TestAuthHandler.SchemeName);
        var handler = (TestAuthHandler)_factory.Services.GetRequiredService(scheme!.HandlerType);

        handler.TestUser = new ClaimsPrincipal(new ClaimsIdentity(claims, TestAuthHandler.SchemeName));
        return client;
    }

    [TestMethod]
    public async Task GET_Attendance_RequiresAuth_Returns401_WhenNoToken()
    {
        var client = _factory.CreateClient(); // No auth configured
        var response = await client.GetAsync("/api/attendance");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [TestMethod]
    public async Task GET_Attendance_Returns403_WhenWrongRole()
    {
        var client = await GetClientAsync(new Claim(ClaimTypes.Role, "Intern"));
        var response = await client.GetAsync("/api/attendance/1");
        Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [TestMethod]
    public async Task GET_Attendance_Returns200_WhenAdminRole()
    {
        var client = await GetClientAsync(
            new Claim(ClaimTypes.NameIdentifier, "user-123"),
            new Claim(ClaimTypes.Role, "Admin"));

        var response = await client.GetAsync("/api/attendance/1");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task GET_Attendance_Returns200_WhenOwnRecord()
    {
        var client = await GetClientAsync(
            new Claim(ClaimTypes.NameIdentifier, "user-123"),
            new Claim(ClaimTypes.Role, "Employee"));

        var response = await client.GetAsync("/api/attendance/1");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}