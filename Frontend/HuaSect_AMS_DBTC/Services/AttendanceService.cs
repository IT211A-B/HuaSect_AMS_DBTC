using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AttendanceService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<ICollection<Attendance>> GetAllAttendanceRecordsAsync()
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Attendance");
            response.EnsureSuccessStatusCode();
            var attendanceRecords = await response.Content.ReadFromJsonAsync<ICollection<Attendance>>();
            return attendanceRecords;
        }

        public async Task<ICollection<Attendance>> GetStudentAttendanceRecordsAsync(int studentId)
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Attendance?studentId={studentId}");
            response.EnsureSuccessStatusCode();
            var attendanceRecords = await response.Content.ReadFromJsonAsync<ICollection<Attendance>>();
            return attendanceRecords;
        }
    }
}
