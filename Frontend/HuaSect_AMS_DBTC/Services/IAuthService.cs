using HuaSect_AMS_DBTC.Models;
namespace HuaSect_AMS_DBTC.Services
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(string username, string password, string role);
    }
}