using HuaSect_AMS_DBTCclasslib.Dtos;
using HuaSect_AMS_DBTCclasslib.Helpers;
using HuaSect_AMS_DBTCclasslib.Models;
using HuaSect_AMS_DBTC.Repository;
using Microsoft.AspNetCore.Identity;
using HuaSect_AMS_DBTCclasslib;

namespace HuaSect_AMS_DBTC.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentService(IStudentRepository repository, UserManager<ApplicationUser> userManager) {
            _repository = repository;
            _userManager = userManager;
        }

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
            var newStudent = new StudentProfile(dto.StudentNumber, dto.YearLevel);
            await _repository.AddAsync(newStudent);
            await _repository.SaveChangesAsync();
            

            return new NewlyCreateStudentDto(newStudent.ID, newStudent.YearLevel);
        }

        public async Task UpdateStudentAsync(int id, UpdateStudentDto dto)
        {
            if (id != dto.Id)
                throw new ArgumentException("Student ID mismatch", nameof(id));

            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new KeyNotFoundException($"Student with id = {id} not found");
            
            var user = await _userManager.FindByIdAsync(student.User.Id);
            if (user == null)
                throw new KeyNotFoundException($"Teacher with id = {id} not found");
            user.FirstName = dto.FirstName ?? user.FirstName;
            user.LastName = dto.LastName ?? user.LastName;
            user.MiddleName = dto.MiddleName ?? user.MiddleName;
            user.Suffix = dto.Suffix ?? user.Suffix;
            var userResult = await _userManager.UpdateAsync(user);
            if  (!userResult.Succeeded)
                throw new Exception(userResult.Errors.ToString());

            student.Update(dto.Id, dto.StudentNumber, dto.YearLevel);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateStudentSelectivelyAsync(int id, UpdateStudentDto patchedDto)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new KeyNotFoundException($"Student with id = {id} not found");
            
            var user = await _userManager.FindByIdAsync(student.User.Id);
            if (user == null)
                throw new KeyNotFoundException($"Teacher with id = {id} not found");
            user.FirstName = patchedDto.FirstName ?? user.FirstName;
            user.LastName = patchedDto.LastName ?? user.LastName;
            user.MiddleName = patchedDto.MiddleName ?? user.MiddleName;
            user.Suffix = patchedDto.Suffix ?? user.Suffix;
            var userResult = await _userManager.UpdateAsync(user);
            if  (!userResult.Succeeded)
                throw new Exception(userResult.Errors.ToString());

            student.Update(patchedDto.Id, patchedDto.StudentNumber, patchedDto.YearLevel);
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