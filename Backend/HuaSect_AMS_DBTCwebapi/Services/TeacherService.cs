using HuaSect_AMS_DBTCclasslib.Helpers;
using HuaSect_AMS_DBTC.Repository;
using HuaSect_AMS_DBTCclasslib;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HuaSect_AMS_DBTC.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherService(ITeacherRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<List<TeacherProfile>> GetAllTeachersAsync() => await _repository.GetAllAsync();

        public async Task<PagedResult<TeacherProfile>> GetPaginatedTeachersAsync(int pageNumber, int pageSize)
        {
            var (total, data) = await _repository.GetPaginatedAsync(pageNumber, pageSize);
            return new PagedResult<TeacherProfile>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total
            };
        }

        public async Task<TeacherProfile?> GetTeacherByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<NewlyCreateTeacherDto> CreateTeacherAsync(CreateTeacherDto dto)
        {
            var newTeacher = new TeacherProfile(dto.Department);
            var newlyCreatedTeacher = await _repository.AddAsync(newTeacher);
            await _repository.SaveChangesAsync();

            return new NewlyCreateTeacherDto(newlyCreatedTeacher.User.Id, newlyCreatedTeacher.User.FirstName, newlyCreatedTeacher.User.LastName, newlyCreatedTeacher.User.MiddleName, newlyCreatedTeacher.User.Suffix, newTeacher.Department);
        }

        public async Task UpdateTeacherAsync(int id, UpdateTeacherDto dto)
        {
            if (id != dto.Id)
                throw new ArgumentException("Teacher ID mismatch", nameof(id));

            var teacher = await _repository.GetByIdAsync(id);
            if (teacher == null)
                throw new KeyNotFoundException($"Teacher with id = {id} not found");

            var user = await _userManager.FindByIdAsync(teacher.User.Id);
            if (user == null)
                throw new KeyNotFoundException($"Teacher with id = {id} not found");
            user.FirstName = dto.FirstName ?? user.FirstName;
            user.LastName = dto.LastName ?? user.LastName;
            user.MiddleName = dto.MiddleName ?? user.MiddleName;
            user.Suffix = dto.Suffix ?? user.Suffix;
            var userResult = await _userManager.UpdateAsync(user);
            if (!userResult.Succeeded)
                throw new Exception(userResult.Errors.ToString());

            teacher.Update(dto.Id, dto.Department);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateTeacherSelectivelyAsync(int id, UpdateTeacherDto patchedDto)
        {
            var teacher = await _repository.GetByIdAsync(id);
            if (teacher == null)
                throw new KeyNotFoundException($"Teacher with id = {id} not found");

            var user = await _userManager.FindByIdAsync(teacher.User.Id);
            if (user == null)
                throw new KeyNotFoundException($"Teacher with id = {id} not found");
            user.FirstName = patchedDto.FirstName ?? user.FirstName;
            user.LastName = patchedDto.LastName ?? user.LastName;
            user.MiddleName = patchedDto.MiddleName ?? user.MiddleName;
            user.Suffix = patchedDto.Suffix ?? user.Suffix;
            var userResult = await _userManager.UpdateAsync(user);
            if (!userResult.Succeeded)
                throw new Exception(userResult.Errors.ToString());

            teacher.Update(patchedDto.Id, patchedDto.Department);
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