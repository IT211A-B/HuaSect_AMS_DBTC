using HuaSect_AMS_DBTCclasslib.Helpers;
using HuaSect_AMS_DBTCclasslib.Dtos;
using HuaSect_AMS_DBTCclasslib.Models;

namespace HuaSect_AMS_DBTC.Service
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<PagedResult<Student>> GetPaginatedStudentsAsync(int pageNumber, int pageSize);
        Task<Student?> GetStudentByIdAsync(int id);
        Task<NewlyCreateStudentDto> CreateStudentAsync(CreateStudentDto dto);
        Task UpdateStudentAsync(int id, UpdateStudentDto dto);
        Task UpdateStudentSelectivelyAsync(int id, UpdateStudentDto patchedDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}