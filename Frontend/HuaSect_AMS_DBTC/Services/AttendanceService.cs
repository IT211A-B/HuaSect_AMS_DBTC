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

        public async Task<ICollection<Attendance>> GetAllAttendanceRecordsAsync(string jwtCookie)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config["BackendUrl"]}/api/Attendance");
            request.Headers.Add("authorization", $"Bearer {jwtCookie}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var attendanceRecords = await response.Content.ReadFromJsonAsync<ICollection<Attendance>>();
            return attendanceRecords;
        }

        public async Task<ICollection<Attendance>> GetStudentAttendanceRecordsAsync(int studentId, string jwtCookie)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config["BackendUrl"]}/api/Attendance?studentId={studentId}");
            request.Headers.Add("authorization", $"Bearer {jwtCookie}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var attendanceRecords = await response.Content.ReadFromJsonAsync<ICollection<Attendance>>();
            return attendanceRecords;
        }
    }
}
