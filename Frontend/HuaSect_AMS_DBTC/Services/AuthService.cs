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

        public async Task<IEnumerable<string>> LoginAsync(LogInModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config["BackendUrl"]}/api/Auth/login", model);
            response.EnsureSuccessStatusCode();
            response.Headers.TryGetValues("Set-Cookie", out var cookies);
            return cookies;
        }
    }
}
