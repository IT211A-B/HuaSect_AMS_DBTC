using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.Helpers;

namespace HuaSect_AMS_DBTC.Service
{
    public interface ITeacherService
    {
        Task<List<TeacherProfile>> GetAllTeachersAsync();
        Task<PagedResult<TeacherProfile>> GetPaginatedTeachersAsync(int pageNumber, int pageSize);
        Task<TeacherProfile?> GetTeacherByIdAsync(int id);
        Task<NewlyCreateTeacherDto> CreateTeacherAsync(CreateTeacherDto dto);
        Task UpdateTeacherAsync(int id, UpdateTeacherDto dto);
        Task UpdateTeacherSelectivelyAsync(int id, UpdateTeacherDto patchedDto);
        Task<bool> DeleteTeacherAsync(int id);
    }
}