using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Repository
{
    public interface ITeacherRepository
    {
        Task<List<TeacherProfile>> GetAllAsync();
        Task<(int TotalRecords, List<TeacherProfile> Data)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<TeacherProfile?> GetByIdAsync(int id);
        Task<TeacherProfile> AddAsync(TeacherProfile teacherProfile);
        Task UpdateAsync(TeacherProfile teacherProfile);
        Task DeleteAsync(TeacherProfile teacherProfile);
        Task SaveChangesAsync();
    }
}