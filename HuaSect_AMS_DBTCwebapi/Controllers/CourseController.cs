using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DatabaseCtx _context;
        public CourseController(DatabaseCtx context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _context.Course.ToListAsync());
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCourse(CreateCourseDto course)
        {
            var newCourse = new Course { };
            var newlyAddedCourse = (await _context.AddAsync(newCourse)).Entity;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateCourse), new
            {
                id = newlyAddedCourse.ID
            }, newlyAddedCourse);
        }

        [HttpPut("{id:int}")]
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

            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
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

            course.ID = mapCourseDto.ID;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var courseToDelete = await _context.Course.FirstOrDefaultAsync(dbCourse => dbCourse.ID == id);
            if (courseToDelete == null)
            {
                return NotFound($"Course with id = {id} not found");
            }

            _context.Teacher.Remove(courseToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
