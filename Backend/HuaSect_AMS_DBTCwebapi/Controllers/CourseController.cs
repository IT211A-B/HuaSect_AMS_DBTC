using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDatabaseCtx _context;
        public CourseController(ApplicationDatabaseCtx context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _context.Course.ToListAsync());
        }

        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoursesPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var totalRecords = await _context.Course.CountAsync();

            var data = await _context.Course
                .OrderBy(s => s.ID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new PagedResult<Course>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
            });
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _context.Course.FirstOrDefaultAsync(c => c.ID == id);
            if (course == null)
            {
                return NotFound("Course with id = {id} not found");
            }
            return Ok(course);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCourse(CreateCourseDto course)
        {
            var newCourse = new Course(course.Name, course.Units, course.Schedule);
            var newlyAddedCourse = (await _context.AddAsync(newCourse)).Entity;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateCourse), new NewlyCreateCourseDto
            {
                ID = newlyAddedCourse.ID,
                Name = newlyAddedCourse.Name,
                Schedule = newlyAddedCourse.Schedule,
                Units = newlyAddedCourse.Units
            }, newlyAddedCourse);
        }

        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCourse(int id, UpdateCourseDto course)
        {
            if (id != course.ID)
            {
                return BadRequest("Course ID mismatch");
            }

            var courseToUpdate = await _context.Course.FirstOrDefaultAsync(dbCourse => dbCourse.ID == id);
            if (courseToUpdate == null)
            {
                return NotFound($"Course with id = {id} not found");
            }

            courseToUpdate.Update(course.ID, course.Name, course.Schedule, course.Units);
            _context.Entry(courseToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("update-selectively/{id:int}")]
        public async Task<IActionResult> UpdateCourseSelectively(int id, [FromBody] JsonPatchDocument<UpdateCourseDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var course = await _context.Course.FirstOrDefaultAsync(c => c.ID == id);
            if (course == null)
            {
                return NotFound($"Course with id = {id} not found");
            }

            var mapCourseDto = new UpdateCourseDto { ID = course.ID };
            patchDoc.ApplyTo(mapCourseDto, ModelState);
            if (!TryValidateModel(mapCourseDto))
            {
                return ValidationProblem(ModelState);
            }

            course.Update(mapCourseDto.ID, mapCourseDto.Name, mapCourseDto.Schedule, mapCourseDto.Units);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var courseToDelete = await _context.Course.FirstOrDefaultAsync(dbCourse => dbCourse.ID == id);
            if (courseToDelete == null)
            {
                return NotFound($"Course with id = {id} not found");
            }

            _context.Course.Remove(courseToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
