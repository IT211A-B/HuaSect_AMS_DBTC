using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public StudentService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<ICollection<Student>> GetAllStudentsAsync(string jwtCookie)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config["BackendUrl"]}/api/Student");
            request.Headers.Add("authorization", $"Bearer {jwtCookie}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var students = await response.Content.ReadFromJsonAsync<ICollection<Student>>();
            return students;
        }

        public async Task<Student?> GetStudentByIdAsync(int id, string jwtCookie)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config["BackendUrl"]}/api/Student/{id}");
            request.Headers.Add("authorization", $"Bearer {jwtCookie}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var student = await response.Content.ReadFromJsonAsync<Student>();
            return student;
        }
    }
}
