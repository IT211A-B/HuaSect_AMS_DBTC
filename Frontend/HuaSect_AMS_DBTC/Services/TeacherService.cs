using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class TeacherService : ITeacherService
    {
        Task<AttendanceHistoryViewModel?> ITeacherService.GetAttendanceAsync(int teacherId, int courseId)
        {
            throw new NotImplementedException();
        }

        Task<TeacherDashboardViewModel?> ITeacherService.GetDashboardAsync(int teacherId)
        {
            throw new NotImplementedException();
        }

        Task<Teacher?> ITeacherService.GetTeacherAsync(int teacherId)
        {
            throw new NotImplementedException();
        }
    }
}
