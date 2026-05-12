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
            throw new NotImplementedException();
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
