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

        public async Task<ICollection<Teacher>> GetAllTeachersAsync()
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Teacher");
            response.EnsureSuccessStatusCode();
            var teachers = await response.Content.ReadFromJsonAsync<ICollection<Teacher>>();
            return teachers;
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Teacher/{id}");
            response.EnsureSuccessStatusCode();
            var teacher = await response.Content.ReadFromJsonAsync<Teacher>();
            return teacher;
        }
    }
}
