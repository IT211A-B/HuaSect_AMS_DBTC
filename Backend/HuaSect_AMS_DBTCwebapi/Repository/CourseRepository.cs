using Microsoft.EntityFrameworkCore;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDatabaseCtx _context;

        public CourseRepository(ApplicationDatabaseCtx context) => _context = context;

        public async Task<List<Course>> GetAllAsync() => await _context.Course.ToListAsync();

        public async Task<(int TotalRecords, List<Course> Data)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Course.AsQueryable();
            var totalRecords = await query.CountAsync();
            var data = await query
                .OrderBy(c => c.ID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRecords, data);
        }

        public async Task<Course?> GetByIdAsync(int id) => await _context.Course.FirstOrDefaultAsync(c => c.ID == id);

        public async Task<Course> AddAsync(Course course)
        {
            await _context.Course.AddAsync(course);
            return course;
        }

        public Task UpdateAsync(Course course)
        {
            _context.Course.Update(course);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Course course)
        {
            _context.Course.Remove(course);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}