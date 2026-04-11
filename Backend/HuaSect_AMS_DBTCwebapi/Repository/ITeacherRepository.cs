using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Repository
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllAsync();
        Task<(int TotalRecords, List<Teacher> Data)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<Teacher?> GetByIdAsync(int id);
        Task<Teacher> AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(Teacher teacher);
        Task SaveChangesAsync();
    }
}