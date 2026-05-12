using HuaSect_AMS_DBTC.Models;
using System.Net.Http;

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

        public async Task CreateCourseAsync(CourseModel course)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config["BackendUrl"]}/api/Course", course);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<CourseModel>> GetAllCoursesAsync()
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Course");

            response.EnsureSuccessStatusCode();

            var courses = await response.Content.ReadFromJsonAsync<IEnumerable<CourseModel>>();

            return courses ?? Enumerable.Empty<CourseModel>();
        }

        public async Task<CourseModel?> GetCourseByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Course/{id}");

            response.EnsureSuccessStatusCode();

            var course = await response.Content.ReadFromJsonAsync<CourseModel>();

            return course;
        }
    }
}
