using HuaSect_AMS_DBTC.Models;
namespace HuaSect_AMS_DBTC.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> GetAllStudentsAsync();
        Task<IEnumerable<StudentModel>> GetStudentsByCourseAsync(string courseId);
        Task<StudentModel?> GetStudentByIdAsync(int id);
        Task AddStudentAsync(StudentModel student);
        Task UpdateStudentAsync(StudentModel student);
        Task DeleteStudentAsync(int id);
        Task<IEnumerable<AttendanceRecord>> GetAttendanceByStudentIdAsync(int id);
        Task<StudentListViewModel> GetStudentListViewModelAsync(string course, DateTime date);
        Task SaveAttendanceAsync(List<AttendanceInputModel> records);
    }
}