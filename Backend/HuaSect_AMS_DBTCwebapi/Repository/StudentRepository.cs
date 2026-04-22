using Microsoft.EntityFrameworkCore;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib.Models;

namespace HuaSect_AMS_DBTC.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDatabaseCtx _context;

        public StudentRepository(ApplicationDatabaseCtx context) => _context = context;

        public async Task<List<StudentProfile>> GetAllAsync() => await _context.StudentProfile.ToListAsync();

        public async Task<(int TotalRecords, List<StudentProfile> Data)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.StudentProfile.AsQueryable();
            var totalRecords = await query.CountAsync();
            var data = await query
                .OrderBy(s => s.ID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRecords, data);
        }

        public async Task<StudentProfile?> GetByIdAsync(int id) => await _context.StudentProfile.FirstOrDefaultAsync(s => s.ID == id);

        public async Task<StudentProfile> AddAsync(StudentProfile studentProfile)
        {
            await _context.StudentProfile.AddAsync(studentProfile);
            return studentProfile;
        }

        public Task UpdateAsync(StudentProfile studentProfile)
        {
            _context.StudentProfile.Update(studentProfile);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(StudentProfile studentProfile)
        {
        _context.StudentProfile.Remove(studentProfile);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}