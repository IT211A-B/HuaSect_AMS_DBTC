using HuaSect_AMS_DBTCclasslib.Models;

namespace HuaSect_AMS_DBTC.Repository
{
    public interface IStudentRepository
    {
        Task<List<StudentProfile>> GetAllAsync();
        Task<(int TotalRecords, List<StudentProfile> Data)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<StudentProfile?> GetByIdAsync(int id);
        Task<StudentProfile> AddAsync(StudentProfile studentProfile);
        Task UpdateAsync(StudentProfile studentProfile);
        Task DeleteAsync(StudentProfile studentProfile);
        Task SaveChangesAsync();
    }
}