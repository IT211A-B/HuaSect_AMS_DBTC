using HuaSect_AMS_DBTCclasslib.Helpers;
using HuaSect_AMS_DBTC.Repository;
using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repository;

        public TeacherService(ITeacherRepository repository) => _repository = repository;

        public async Task<List<Teacher>> GetAllTeachersAsync() => await _repository.GetAllAsync();

        public async Task<PagedResult<Teacher>> GetPaginatedTeachersAsync(int pageNumber, int pageSize)
        {
            var (total, data) = await _repository.GetPaginatedAsync(pageNumber, pageSize);
            return new PagedResult<Teacher>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total
            };
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<NewlyCreateTeacherDto> CreateTeacherAsync(CreateTeacherDto dto)
        {
            var newTeacher = new Teacher(dto.FirstName, dto.LastName, dto.Email, dto.Department, dto.PhoneNumber);
            await _repository.AddAsync(newTeacher);
            await _repository.SaveChangesAsync();

            return new NewlyCreateTeacherDto
            {
                ID = newTeacher.ID,
                Department = newTeacher.Department,
                Email = newTeacher.Email,
                FirstName = newTeacher.FirstName,
                LastName = newTeacher.LastName,
                PhoneNumber = newTeacher.PhoneNumber,
            };
        }

        public async Task UpdateTeacherAsync(int id, UpdateTeacherDto dto)
        {
            if (id != dto.ID)
                throw new ArgumentException("Teacher ID mismatch", nameof(id));

            var teacher = await _repository.GetByIdAsync(id);
            if (teacher == null)
                throw new KeyNotFoundException($"Teacher with id = {id} not found");

            teacher.Update(dto.ID, dto.FirstName, dto.LastName, dto.Email, dto.Department, dto.PhoneNumber);
            await _repository.SaveChangesAsync();
        }

        // Assumes validation & patch application happened in the controller
        public async Task UpdateTeacherSelectivelyAsync(int id, UpdateTeacherDto patchedDto)
        {
            var teacher = await _repository.GetByIdAsync(id);
            if (teacher == null)
                throw new KeyNotFoundException($"Teacher with id = {id} not found");

            teacher.Update(patchedDto.ID, patchedDto.FirstName, patchedDto.LastName, patchedDto.Email, patchedDto.Department, patchedDto.PhoneNumber);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var teacher = await _repository.GetByIdAsync(id);
            if (teacher == null)
                throw new KeyNotFoundException($"Teacher with id = {id} not found");

            await _repository.DeleteAsync(teacher);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}