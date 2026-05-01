using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceRecord>> GetRecordsAsync(string courseId, DateTime date);
        Task<IEnumerable<AttendanceRecord>> GetRecordsByStudentAsync(string studentId);
        Task SaveRecordsAsync(IEnumerable<AttendanceRecord> records);
    }
}