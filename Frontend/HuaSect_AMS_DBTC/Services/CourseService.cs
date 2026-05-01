using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class CourseService : ICourseService
    {
        Task ICourseService.CreateCourseAsync(CourseModel course)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<CourseModel>> ICourseService.GetAllCoursesAsync()
        {
            return [
            new CourseModel{},
            new CourseModel{ },
            new CourseModel{ },
            new CourseModel{ },
            new CourseModel{ }
            ];
        }

        Task<CourseModel?> ICourseService.GetCourseByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
