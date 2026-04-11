using HuaSect_AMS_DBTCclasslib.Models;

namespace HuaSect_AMS_DBTC.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<(int TotalRecords, List<Student> Data)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<Student?> GetByIdAsync(int id);
        Task<Student> AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task SaveChangesAsync();
    }
}