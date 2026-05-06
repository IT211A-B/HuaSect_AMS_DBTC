using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class TeacherService : ITeacherService
    {
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
            throw new NotImplementedException();
        }
    }
}
