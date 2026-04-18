using HuaSect_AMS_DBTCclasslib.Dtos;
using HuaSect_AMS_DBTCclasslib.Helpers;
using HuaSect_AMS_DBTCclasslib.Models;
using HuaSect_AMS_DBTC.Repository;

namespace HuaSect_AMS_DBTC.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository) => _repository = repository;

        public async Task<List<StudentProfile>> GetAllStudentsAsync() => await _repository.GetAllAsync();

        public async Task<PagedResult<StudentProfile>> GetPaginatedStudentsAsync(int pageNumber, int pageSize)
        {
            var (total, data) = await _repository.GetPaginatedAsync(pageNumber, pageSize);
            return new PagedResult<StudentProfile>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total
            };
        }

        public async Task<StudentProfile?> GetStudentByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<NewlyCreateStudentDto> CreateStudentAsync(CreateStudentDto dto)
        {
            var newStudent = new StudentProfile(dto.Number, dto.YearLevel);
            await _repository.AddAsync(newStudent);
            await _repository.SaveChangesAsync();

            return new NewlyCreateStudentDto
            {
                ID = newStudent.ID,
                YearLevel = newStudent.YearLevel
            };
        }

        public async Task UpdateStudentAsync(int id, UpdateStudentDto dto)
        {
            if (id != dto.ID)
                throw new ArgumentException("Student ID mismatch", nameof(id));

            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new KeyNotFoundException($"Student with id = {id} not found");

            student.Update(dto.ID, dto.Number, dto.YearLevel);
            await _repository.SaveChangesAsync();
        }

        // Assumes validation & patch application happened in the controller
        public async Task UpdateStudentSelectivelyAsync(int id, UpdateStudentDto patchedDto)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new KeyNotFoundException($"Student with id = {id} not found");

            student.Update(patchedDto.ID, patchedDto.Number, patchedDto.YearLevel);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new KeyNotFoundException($"Student with id = {id} not found");

            await _repository.DeleteAsync(student);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}