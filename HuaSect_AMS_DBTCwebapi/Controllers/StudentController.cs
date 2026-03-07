using System.Threading.Tasks;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib.Dtos;
using HuaSect_AMS_DBTCclasslib.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DatabaseCtx _context;
        public StudentController(DatabaseCtx context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await _context.Student.ToListAsync());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Student.FirstOrDefaultAsync(s => s.ID == id);
            if (student == null)
            {
                return NotFound("Student with id = {id} not found");
            }
            return Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateStudent(CreateStudentDto student)
        {
            var newStudent = new Student { };
            var newlyAddedStudent = (await _context.AddAsync(newStudent)).Entity;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateStudent), new
            {
                id = newlyAddedStudent.ID
            }, newlyAddedStudent);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto student)
        {
            if (id != student.ID)
            {
                return BadRequest("Student ID mismatch");
            }

            var studentToUpdate = await _context.Student.FirstOrDefaultAsync(dbStudent => dbStudent.ID == id);
            if (studentToUpdate == null)
            {
                return NotFound($"Student with id = {id} not found");
            }

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateStudentSelectively(int id, [FromBody] JsonPatchDocument<UpdateStudentDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var student = await _context.Student.FirstOrDefaultAsync(s => s.ID == id);
            if (student == null)
            {
                return NotFound($"Student with id = {id} not found");
            }

            var mapStudentDto = new UpdateStudentDto { ID = student.ID };
            patchDoc.ApplyTo(mapStudentDto, ModelState);
            if (!TryValidateModel(mapStudentDto))
            {
                return ValidationProblem(ModelState);
            }

            student.ID = mapStudentDto.ID;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var studentToDelete = await _context.Student.FirstOrDefaultAsync(dbStudent => dbStudent.ID == id);
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
