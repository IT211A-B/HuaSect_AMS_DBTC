using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Repository
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<(int TotalRecords, List<Course> Data)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<Course?> GetByIdAsync(int id);
        Task<Course> AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Course course);
        Task SaveChangesAsync();
    }
}