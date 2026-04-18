using Microsoft.EntityFrameworkCore;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDatabaseCtx _context;

        public TeacherRepository(ApplicationDatabaseCtx context) => _context = context;

        public async Task<List<TeacherProfile>> GetAllAsync() => await _context.TeacherProfile.ToListAsync();

        public async Task<(int TotalRecords, List<TeacherProfile> Data)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.TeacherProfile.AsQueryable();
            var totalRecords = await query.CountAsync();
            var data = await query
                .OrderBy(t => t.ID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRecords, data);
        }

        public async Task<TeacherProfile?> GetByIdAsync(int id) => await _context.TeacherProfile.FirstOrDefaultAsync(t => t.ID == id);

        public async Task<TeacherProfile> AddAsync(TeacherProfile teacherProfile)
        {
            await _context.TeacherProfile.AddAsync(teacherProfile);
            return teacherProfile;
        }

        public Task UpdateAsync(TeacherProfile teacherProfile)
        {
            _context.TeacherProfile.Update(teacherProfile);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TeacherProfile teacherProfile)
        {
            _context.TeacherProfile.Remove(teacherProfile);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}