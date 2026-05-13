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

        public async Task<ICollection<Course>> GetAllCoursesAsync()
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Course");
            response.EnsureSuccessStatusCode();
            var courses = await response.Content.ReadFromJsonAsync<ICollection<Course>>();
            return courses;
        }
    }
}
