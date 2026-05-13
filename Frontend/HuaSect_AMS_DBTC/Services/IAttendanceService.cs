using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public interface IAttendanceService
    {
        Task<ICollection<Attendance>> GetAllAttendanceRecordsAsync();

        Task<ICollection<Attendance>> GetStudentAttendanceRecordsAsync(int studentId);
    }
}