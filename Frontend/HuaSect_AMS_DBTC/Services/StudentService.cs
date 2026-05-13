using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public StudentService(HttpClient httpClient, IConfiguration config) {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<ICollection<Student>> GetAllStudentsAsync()
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Student");
            response.EnsureSuccessStatusCode();
            var students = await response.Content.ReadFromJsonAsync<ICollection<Student>>();
            return students;
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Student/{id}");
            response.EnsureSuccessStatusCode();
            var student = await response.Content.ReadFromJsonAsync<Student>();
            return student;
        }
    }
}
