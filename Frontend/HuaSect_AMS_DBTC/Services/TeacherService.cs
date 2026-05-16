using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public TeacherService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<ICollection<Teacher>> GetAllTeachersAsync(string jwtCookie)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config["BackendUrl"]}/api/Teacher");
            request.Headers.Add("authorization", $"Bearer {jwtCookie}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var teachers = await response.Content.ReadFromJsonAsync<ICollection<Teacher>>();
            return teachers;
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id, string jwtCookie)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config["BackendUrl"]}/api/Teacher/{id}");
            request.Headers.Add("authorization", $"Bearer {jwtCookie}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var teacher = await response.Content.ReadFromJsonAsync<Teacher>();
            return teacher;
        }
    }
}
