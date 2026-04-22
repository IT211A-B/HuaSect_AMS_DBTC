using Microsoft.EntityFrameworkCore;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDatabaseCtx _context;

        public AttendanceRepository(ApplicationDatabaseCtx context) => _context = context;

        public async Task<List<Attendance>> GetAllAsync() => await _context.Attendance.ToListAsync();

        public async Task<(int TotalRecords, List<Attendance> Data)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Attendance.AsQueryable();
            var totalRecords = await query.CountAsync();
            var data = await query
                .OrderBy(c => c.ID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRecords, data);
        }

        public async Task<Attendance?> GetByIdAsync(int id) => await _context.Attendance.FirstOrDefaultAsync(c => c.ID == id);

        public async Task<Attendance> AddAsync(Attendance attendance)
        {
            await _context.Attendance.AddAsync(attendance);
            return attendance;
        }

        public Task UpdateAsync(Attendance attendance)
        {
            _context.Attendance.Update(attendance);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Attendance attendance)
        {
            _context.Attendance.Remove(attendance);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}