using Microsoft.EntityFrameworkCore;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDatabaseCtx _context;

        public TeacherRepository(ApplicationDatabaseCtx context) => _context = context;

        public async Task<List<Teacher>> GetAllAsync() => await _context.Teacher.ToListAsync();

        public async Task<(int TotalRecords, List<Teacher> Data)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Teacher.AsQueryable();
            var totalRecords = await query.CountAsync();
            var data = await query
                .OrderBy(t => t.ID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRecords, data);
        }

        public async Task<Teacher?> GetByIdAsync(int id) => await _context.Teacher.FirstOrDefaultAsync(t => t.ID == id);

        public async Task<Teacher> AddAsync(Teacher teacher)
        {
            await _context.Teacher.AddAsync(teacher);
            return teacher;
        }

        public Task UpdateAsync(Teacher teacher)
        {
            _context.Teacher.Update(teacher);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Teacher teacher)
        {
            _context.Teacher.Remove(teacher);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}