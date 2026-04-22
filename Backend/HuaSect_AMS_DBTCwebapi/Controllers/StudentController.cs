using HuaSect_AMS_DBTCclasslib.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using HuaSect_AMS_DBTC.Service;
using Microsoft.AspNetCore.Authorization;

namespace HuaSect_AMS_DBTC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService) => _studentService = studentService;

        [HttpGet]
        [Authorize(Roles = "Student,Teacher,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("paginated")]
        [Authorize(Roles = "Student,Teacher,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentsPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _studentService.GetPaginatedStudentsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Student,Teacher,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudent(int id)
        {
<<<<<<< HEAD
            var student = await _studentService.GetStudentByIdAsync(id);
            return student == null ? NotFound($"Student with id = {id} not found") : Ok(student);
=======
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
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
        }

        [HttpPut("update/{id:int}")]
        [Authorize(Roles = "Admin,Student")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto studentDto)
        {
            if (id != studentDto.Id)
                return BadRequest("Student ID mismatch");

            try
            {
                await _studentService.UpdateStudentAsync(id, studentDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
<<<<<<< HEAD
=======

            studentToUpdate.Update(student.ID, student.Email, student.FirstName, student.LastName, student.MiddleName, student.Suffix, student.YearLevel);
            _context.Entry(studentToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
        }

        [HttpPatch("update-selective/{id:int}")]
        [Authorize(Roles = "Admin,Student")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudentSelectively(int id, [FromBody] JsonPatchDocument<UpdateStudentDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Patch document cannot be null");

            var existingStudent = await _studentService.GetStudentByIdAsync(id);
            if (existingStudent == null)
                return NotFound($"Student with id = {id} not found");

            var dtoToPatch = new UpdateStudentDto { Id = existingStudent.ID };
            patchDoc.ApplyTo(dtoToPatch, ModelState);

            if (!TryValidateModel(dtoToPatch))
                return ValidationProblem(ModelState);

            try
            {
                await _studentService.UpdateStudentSelectivelyAsync(id, dtoToPatch);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
<<<<<<< HEAD
=======

            student.Update(mapStudentDto.ID, mapStudentDto.Email, mapStudentDto.FirstName, mapStudentDto.LastName, mapStudentDto.MiddleName, mapStudentDto.Suffix, mapStudentDto.YearLevel);
            await _context.SaveChangesAsync();

            return NoContent();
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "Admin,Student")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}