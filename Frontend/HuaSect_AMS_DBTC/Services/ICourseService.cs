using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public interface ICourseService
    {
        Task<ICollection<Course>> GetAllCoursesAsync();
    }
}