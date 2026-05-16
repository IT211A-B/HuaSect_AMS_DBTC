using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public CourseService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task CreateCourse(CreateCourseModel model, string jwtCookie)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config["BackendUrl"]}/api/Course");
            request.Headers.Add("authorization", $"Bearer {jwtCookie}");
            request.Content = JsonContent.Create(model);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<ICollection<Course>> GetAllCoursesAsync(string jwtCookie)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config["BackendUrl"]}/api/Course");
            request.Headers.Add("authorization", $"Bearer {jwtCookie}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var courses = await response.Content.ReadFromJsonAsync<ICollection<Course>>();
            return courses;
        }

        public async Task<Course?> GetCourseByIdAsync(int id, string jwtCookie)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config["BackendUrl"]}/api/Course/{id}");
            request.Headers.Add("authorization", $"Bearer {jwtCookie}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var course = await response.Content.ReadFromJsonAsync<Course>();
            return course;
        }
    }
}
