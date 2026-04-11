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

        public async Task<List<Student>> GetAllStudentsAsync() => await _repository.GetAllAsync();

        public async Task<PagedResult<Student>> GetPaginatedStudentsAsync(int pageNumber, int pageSize)
        {
            var (total, data) = await _repository.GetPaginatedAsync(pageNumber, pageSize);
            return new PagedResult<Student>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total
            };
        }

        public async Task<Student?> GetStudentByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<NewlyCreateStudentDto> CreateStudentAsync(CreateStudentDto dto)
        {
            var newStudent = new Student(dto.Email, dto.FirstName, dto.LastName, dto.MiddleName, dto.Suffix, dto.YearLevel);
            await _repository.AddAsync(newStudent);
            await _repository.SaveChangesAsync();

            return new NewlyCreateStudentDto
            {
                ID = newStudent.ID,
                Email = newStudent.Email,
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                MiddleName = newStudent.MiddleName,
                Suffix = newStudent.Suffix,
                FullName = newStudent.FullName,
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

            student.Update(dto.ID, dto.Email, dto.FirstName, dto.LastName, dto.MiddleName, dto.Suffix, dto.YearLevel);
            await _repository.SaveChangesAsync();
        }

        // Assumes validation & patch application happened in the controller
        public async Task UpdateStudentSelectivelyAsync(int id, UpdateStudentDto patchedDto)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new KeyNotFoundException($"Student with id = {id} not found");

            student.Update(patchedDto.ID, patchedDto.Email, patchedDto.FirstName, patchedDto.LastName, patchedDto.MiddleName, patchedDto.Suffix, patchedDto.YearLevel);
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