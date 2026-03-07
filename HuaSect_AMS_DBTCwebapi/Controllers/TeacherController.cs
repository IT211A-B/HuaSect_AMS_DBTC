using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.DbCtx;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTeacher(CreateTeacherDto teacher)
        {
            var newTeacher = new Teacher { };
            var newlyAddedTeacher = (await _context.AddAsync(newTeacher)).Entity;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateTeacher), new
            {
                id = newlyAddedTeacher.ID
            }, newlyAddedTeacher);
        }

        [HttpPut("{id:int}")]
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

            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
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

            teacher.ID = mapTeacherDto.ID;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var studentToDelete = await _context.Teacher.FirstOrDefaultAsync(dbStudent => dbStudent.ID == id);
            if (studentToDelete == null)
            {
                return NotFound($"Student with id = {id} not found");
            }

            _context.Student.Remove(studentToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
