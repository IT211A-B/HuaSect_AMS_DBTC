using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.Helpers;

namespace HuaSect_AMS_DBTC.Service
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllTeachersAsync();
        Task<PagedResult<Teacher>> GetPaginatedTeachersAsync(int pageNumber, int pageSize);
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task<NewlyCreateTeacherDto> CreateTeacherAsync(CreateTeacherDto dto);
        Task UpdateTeacherAsync(int id, UpdateTeacherDto dto);
        Task UpdateTeacherSelectivelyAsync(int id, UpdateTeacherDto patchedDto);
        Task<bool> DeleteTeacherAsync(int id);
    }
}