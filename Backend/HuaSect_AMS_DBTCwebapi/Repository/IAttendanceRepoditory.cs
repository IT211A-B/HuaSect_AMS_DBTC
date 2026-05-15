using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Repository
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAllAsync();
        Task<(int TotalRecords, List<Attendance> Data)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<List<Attendance>> GetAllByStudentAsync(int id);

        Task<Attendance?> GetByIdAsync(int id);
        Task<Attendance> AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task DeleteAsync(Attendance attendance);
        Task SaveChangesAsync();
    }
}