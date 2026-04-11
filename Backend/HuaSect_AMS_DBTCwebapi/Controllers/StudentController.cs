using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib.Dtos;
using HuaSect_AMS_DBTCclasslib.Models;
using HuaSect_AMS_DBTCclasslib.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDatabaseCtx _context;
        public StudentController(ApplicationDatabaseCtx context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await _context.Student.ToListAsync());
        }

        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentsPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var totalRecords = await _context.Student.CountAsync();

            var data = await _context.Student
                .OrderBy(s => s.ID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new PagedResult<Student>
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
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Student.FirstOrDefaultAsync(s => s.ID == id);
            if (student == null)
            {
                return NotFound("Student with id = {id} not found");
            }
            return Ok(student);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateStudent(CreateStudentDto student)
        {
            var newStudent = new Student(student.Email, student.FirstName, student.LastName, student.MiddleName, student.Suffix, student.YearLevel);
            var newlyAddedStudent = (await _context.AddAsync(newStudent)).Entity;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateStudent), new NewlyCreateStudentDto
            {
                ID = newlyAddedStudent.ID,
                Email = newlyAddedStudent.Email,
                FirstName = newlyAddedStudent.FirstName,
                LastName = newlyAddedStudent.LastName,
                MiddleName = newlyAddedStudent.MiddleName,
                Suffix = newlyAddedStudent.Suffix,
                FullName = newlyAddedStudent.FullName,
                YearLevel = newlyAddedStudent.YearLevel
            }, newlyAddedStudent);
        }

        [HttpPut("update/{id:int}")]
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

            studentToUpdate.Update(student.ID, student.Email, student.FirstName, student.LastName, student.MiddleName, student.Suffix, student.YearLevel);
            _context.Entry(studentToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("update-selective/{id:int}")]
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

            student.Update(mapStudentDto.ID, mapStudentDto.Email, mapStudentDto.FirstName, mapStudentDto.LastName, mapStudentDto.MiddleName, mapStudentDto.Suffix, mapStudentDto.YearLevel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id:int}")]
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
