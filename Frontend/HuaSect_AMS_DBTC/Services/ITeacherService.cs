using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public interface ITeacherService
    {
        Task<Teacher?> GetTeacherAsync(int teacherId);
        Task<TeacherDashboardViewModel?> GetDashboardAsync(int teacherId);
        Task<AttendanceHistoryViewModel?> GetAttendanceAsync(int teacherId, int courseId);
    }
}