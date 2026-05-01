using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class StudentService : IStudentService
    {
        Task IStudentService.AddStudentAsync(StudentModel student)
        {
            throw new NotImplementedException();
        }

        Task IStudentService.DeleteStudentAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<StudentModel>> IStudentService.GetAllStudentsAsync()
        {
            return new StudentModel[5];
        }

        Task<IEnumerable<AttendanceRecord>> IStudentService.GetAttendanceByStudentIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<StudentModel?> IStudentService.GetStudentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<StudentListViewModel> IStudentService.GetStudentListViewModelAsync(string course, DateTime date)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<StudentModel>> IStudentService.GetStudentsByCourseAsync(string courseId)
        {
            throw new NotImplementedException();
        }

        Task IStudentService.SaveAttendanceAsync(List<AttendanceInputModel> records)
        {
            throw new NotImplementedException();
        }

        Task IStudentService.UpdateStudentAsync(StudentModel student)
        {
            throw new NotImplementedException();
        }
    }
}
