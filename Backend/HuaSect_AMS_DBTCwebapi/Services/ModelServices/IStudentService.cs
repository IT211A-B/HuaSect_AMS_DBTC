using HuaSect_AMS_DBTCclasslib.Helpers;
using HuaSect_AMS_DBTCclasslib.Dtos;
using HuaSect_AMS_DBTCclasslib.Models;
using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Service
{
    public interface IStudentService
    {
        Task<List<StudentProfile>> GetAllStudentsAsync();

        Task<List<StudentProfile>> GetAllCourseStudentsAsync(int id);
        Task<PagedResult<StudentProfile>> GetPaginatedStudentsAsync(int pageNumber, int pageSize);
        Task<StudentProfile?> GetStudentByIdAsync(int id);
        Task<NewlyCreateStudentDto> CreateStudentAsync(CreateStudentDto dto, ApplicationUser user);
        Task UpdateStudentAsync(int id, UpdateStudentDto dto);
        Task UpdateStudentSelectivelyAsync(int id, UpdateStudentDto patchedDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}