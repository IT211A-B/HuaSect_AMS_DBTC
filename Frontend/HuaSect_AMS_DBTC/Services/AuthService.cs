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

        public Task<LogInResponseModel> ConfirmEmailAsync(LogInModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config["BackendUrl"]}/api/Auth/confirmEmail?", model);
            response.EnsureSuccessStatusCode();
        }

        public async Task<LogInResponseModel> LoginAsync(LogInModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config["BackendUrl"]}/api/Auth/login", model);
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadFromJsonAsync<LogInResponseModel>();
            return token;
        }

        public Task<LogInResponseModel> RegisterStudentAsync(RegisterStudentModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config["BackendUrl"]}/api/Auth/register/student", model);
            response.EnsureSuccessStatusCode();
        }

        public Task<LogInResponseModel> RegisterTeacherAsync(RegisterTeacherModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config["BackendUrl"]}/api/Auth/register/teacher", model);
            response.EnsureSuccessStatusCode();
        }
    }
}
