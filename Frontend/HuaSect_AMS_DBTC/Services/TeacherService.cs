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

        public async Task<AttendanceHistoryViewModel?> GetAttendanceAsync(int teacherId, int courseId)
        {
            throw new NotImplementedException();
        }

        public async Task<TeacherDashboardViewModel?> GetDashboardAsync(int teacherId)
        {
            var teacherResponse = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Teacher/{teacherId}");

            teacherResponse.EnsureSuccessStatusCode();

            var teacher = await teacherResponse.Content.ReadFromJsonAsync<Teacher>();
            if (teacher is null)
                throw new Exception("Error: teacher not found");

            var coursesResponse = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Course");
            coursesResponse.EnsureSuccessStatusCode();
            var courses = ((await coursesResponse.Content.ReadFromJsonAsync<IEnumerable<CourseCardViewModel>>()) ?? []).ToList();

            return new TeacherDashboardViewModel
            {
                Teacher = teacher,
                Courses = courses,
                CoursesToday = 0, // dont know what to do with this
            };
        }

        public async Task<Teacher?> GetTeacherAsync(int teacherId)
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Teacher/{teacherId}");

            response.EnsureSuccessStatusCode();

            var teacher = await response.Content.ReadFromJsonAsync<Teacher>();

            return teacher;
        }
    }
}
