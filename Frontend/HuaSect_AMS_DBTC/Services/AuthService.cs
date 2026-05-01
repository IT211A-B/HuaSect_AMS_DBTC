using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class AuthService : IAuthService
    {
        Task<AuthResult> IAuthService.AuthenticateAsync(string username, string password, string role)
        {
            throw new NotImplementedException();
        }
    }
}
