using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AuthService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<AuthResult> AuthenticateAsync(string username, string password, string role)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config["BackendUrl"]}/api/Auth", new {
                Username = username,
                Password = password,
                Role = role
            });
            
            response.EnsureSuccessStatusCode();

            var authRes = await response.Content.ReadFromJsonAsync<AuthResult>();

            return authRes ?? new AuthResult { };
        }
    }
}
