using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly DatabaseCtx _context;
        public TeacherController(DatabaseCtx context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            return Ok(await _context.Teacher.ToListAsync());
        }

        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeachersPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var totalRecords = await _context.Teacher.CountAsync();

            var data = await _context.Teacher
                .OrderBy(s => s.ID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new PagedResult<Teacher>
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
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _context.Teacher.FirstOrDefaultAsync(s => s.ID == id);
            if (teacher == null)
            {
                return NotFound("Teacher with id = {id} not found");
            }
            return Ok(teacher);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTeacher(CreateTeacherDto teacher)
        {
            var newTeacher = new Teacher
            {
                Department = teacher.Department,
                Email = teacher.Email,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MiddleName = teacher.MiddleName,
                PhoneNumber = teacher.PhoneNumber,
                Suffix = teacher.Suffix
            };
            var newlyAddedTeacher = (await _context.AddAsync(newTeacher)).Entity;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateTeacher), new NewlyCreateTeacherDto
            {
                ID = newlyAddedTeacher.ID,
                Department = newlyAddedTeacher.Department,
                Email = newlyAddedTeacher.Email,
                FirstName = newlyAddedTeacher.FirstName,
                LastName = newlyAddedTeacher.LastName,
                MiddleName = newlyAddedTeacher.MiddleName,
                PhoneNumber = newlyAddedTeacher.PhoneNumber,
                Suffix = newlyAddedTeacher.Suffix
            }, newlyAddedTeacher);
        }

        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeacher(int id, UpdateTeacherDto teacher)
        {
            if (id != teacher.ID)
            {
                return BadRequest("Teacher ID mismatch");
            }

            var teacherToUpdate = await _context.Teacher.FirstOrDefaultAsync(dbTeacher => dbTeacher.ID == id);
            if (teacherToUpdate == null)
            {
                return NotFound($"Teacher with id = {id} not found");
            }
            teacherToUpdate.Department = teacher.Department;
            teacherToUpdate.Email = teacher.Email;
            teacherToUpdate.FirstName = teacher.FirstName;
            teacherToUpdate.LastName = teacher.LastName;
            teacherToUpdate.MiddleName = teacher.MiddleName;
            teacherToUpdate.Suffix = teacher.Suffix;
            teacherToUpdate.Department = teacher.Department;
            teacherToUpdate.PhoneNumber = teacher.PhoneNumber;

            _context.Entry(teacherToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("update-selectively/{id:int}")]
        public async Task<IActionResult> UpdateTeacherSelectively(int id, [FromBody] JsonPatchDocument<UpdateTeacherDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var teacher = await _context.Teacher.FirstOrDefaultAsync(s => s.ID == id);
            if (teacher == null)
            {
                return NotFound($"Teacher with id = {id} not found");
            }

            var mapTeacherDto = new UpdateTeacherDto { ID = teacher.ID };
            patchDoc.ApplyTo(mapTeacherDto, ModelState);
            if (!TryValidateModel(mapTeacherDto))
            {
                return ValidationProblem(ModelState);
            }

            teacher.FirstName = mapTeacherDto.FirstName;
            teacher.MiddleName = mapTeacherDto.MiddleName;
            teacher.LastName = mapTeacherDto.LastName;
            teacher.Suffix = mapTeacherDto.Suffix;
            teacher.Email = mapTeacherDto.Email;
            teacher.PhoneNumber = mapTeacherDto.PhoneNumber;
            teacher.Department = mapTeacherDto.Department;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var teacherToDelete = await _context.Teacher.FirstOrDefaultAsync(dbStudent => dbStudent.ID == id);
            if (teacherToDelete == null)
            {
                return NotFound($"Student with id = {id} not found");
            }

            _context.Teacher.Remove(teacherToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
