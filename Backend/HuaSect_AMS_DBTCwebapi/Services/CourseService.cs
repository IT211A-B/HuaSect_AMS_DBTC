using HuaSect_AMS_DBTC.Repository;
using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.Helpers;
using HuaSect_AMS_DBTC.Service;

namespace HuaSect_AMS_DBTC.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;

        public CourseService(ICourseRepository repository) => _repository = repository;

        public async Task<List<Course>> GetAllCoursesAsync() => await _repository.GetAllAsync();

        public async Task<PagedResult<Course>> GetPaginatedCoursesAsync(int pageNumber, int pageSize)
        {
            var (total, data) = await _repository.GetPaginatedAsync(pageNumber, pageSize);
            return new PagedResult<Course>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total
            };
        }

        public async Task<Course?> GetCourseByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<NewlyCreateCourseDto> CreateCourseAsync(CreateCourseDto dto)
        {
            var newCourse = new Course(dto.Name, dto.Units, dto.Schedule);
            await _repository.AddAsync(newCourse);
            await _repository.SaveChangesAsync();

            return new NewlyCreateCourseDto
            {
                ID = newCourse.ID,
                Name = newCourse.Name,
                Schedule = newCourse.Schedule,
                Units = newCourse.Units
            };
        }

        public async Task UpdateCourseAsync(int id, UpdateCourseDto dto)
        {
            if (id != dto.ID)
                throw new ArgumentException("Course ID mismatch", nameof(id));

            var course = await _repository.GetByIdAsync(id);
            if (course == null)
                throw new KeyNotFoundException($"Course with id = {id} not found");

            course.Update(dto.ID, dto.Name, dto.Schedule, dto.Units);
            await _repository.SaveChangesAsync();
        }

        // This method assumes validation & patch application happened in the controller
        public async Task UpdateCourseSelectivelyAsync(int id, UpdateCourseDto patchedDto)
        {
            var course = await _repository.GetByIdAsync(id);
            if (course == null)
                throw new KeyNotFoundException($"Course with id = {id} not found");

            course.Update(patchedDto.ID, patchedDto.Name, patchedDto.Schedule, patchedDto.Units);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _repository.GetByIdAsync(id);
            if (course == null)
                throw new KeyNotFoundException($"Course with id = {id} not found");

            await _repository.DeleteAsync(course);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}