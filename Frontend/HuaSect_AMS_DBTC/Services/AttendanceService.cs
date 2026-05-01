using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class AttendanceService : IAttendanceService
    {
        Task<IEnumerable<AttendanceRecord>> IAttendanceService.GetRecordsAsync(string courseId, DateTime date)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<AttendanceRecord>> IAttendanceService.GetRecordsByStudentAsync(string studentId)
        {
            throw new NotImplementedException();
        }

        Task IAttendanceService.SaveRecordsAsync(IEnumerable<AttendanceRecord> records)
        {
            throw new NotImplementedException();
        }
    }
}
