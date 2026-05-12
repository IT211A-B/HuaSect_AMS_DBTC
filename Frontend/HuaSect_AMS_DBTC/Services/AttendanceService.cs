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

        public async Task<IEnumerable<AttendanceRecord>> GetRecordsAsync(string courseId, DateTime date)
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Attendance");

            response.EnsureSuccessStatusCode();

            var attendanceRecords = await response.Content.ReadFromJsonAsync<IEnumerable<AttendanceRecord>>();

            return attendanceRecords ?? Enumerable.Empty<AttendanceRecord>();
        }

        public async Task<IEnumerable<AttendanceRecord>> GetRecordsByStudentAsync(string studentId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveRecordsAsync(IEnumerable<AttendanceRecord> records)
        {
            foreach (var record in records) {
                var response = await _httpClient.PostAsJsonAsync($"{_config["BackendUrl"]}/api/Attendance", record);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
