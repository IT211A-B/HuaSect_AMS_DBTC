using Microsoft.EntityFrameworkCore;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib.Models;

namespace HuaSect_AMS_DBTC.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDatabaseCtx _context;

        public StudentRepository(ApplicationDatabaseCtx context) => _context = context;

        public async Task<List<Student>> GetAllAsync() => await _context.Student.ToListAsync();

        public async Task<(int TotalRecords, List<Student> Data)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Student.AsQueryable();
            var totalRecords = await query.CountAsync();
            var data = await query
                .OrderBy(s => s.ID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRecords, data);
        }

        public async Task<Student?> GetByIdAsync(int id) => await _context.Student.FirstOrDefaultAsync(s => s.ID == id);

        public async Task<Student> AddAsync(Student student)
        {
            await _context.Student.AddAsync(student);
            return student;
        }

        public Task UpdateAsync(Student student)
        {
            _context.Student.Update(student);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Student student)
        {
            _context.Student.Remove(student);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}