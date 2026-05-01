// ICourseService.cs — remove:
using HuaSect_AMS_DBTC.Models;
namespace HuaSect_AMS_DBTC.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseModel>> GetAllCoursesAsync();
        Task<CourseModel?> GetCourseByIdAsync(string id);
        Task CreateCourseAsync(CourseModel course);
    }
}