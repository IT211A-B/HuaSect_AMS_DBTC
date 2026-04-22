using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.Helpers;

namespace HuaSect_AMS_DBTC.Service
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<PagedResult<Course>> GetPaginatedCoursesAsync(int pageNumber, int pageSize);
        Task<Course?> GetCourseByIdAsync(int id);
        Task<NewlyCreateCourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task UpdateCourseAsync(int id, UpdateCourseDto dto);
        Task UpdateCourseSelectivelyAsync(int id, UpdateCourseDto patchedDto);
        Task<bool> DeleteCourseAsync(int id);
    }
}