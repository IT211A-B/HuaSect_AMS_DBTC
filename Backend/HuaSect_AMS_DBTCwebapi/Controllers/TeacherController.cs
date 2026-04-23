using HuaSect_AMS_DBTCclasslib.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using HuaSect_AMS_DBTC.Service;
using HuaSect_AMS_DBTCclasslib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HuaSect_AMS_DBTC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService) => _teacherService = teacherService;

        [HttpGet]
        [Authorize(Roles = "Student,Teacher,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeachers()
        {
            var students = await _teacherService.GetAllTeachersAsync();
            return Ok(students);
        }

        [HttpGet("paginated")]
        [Authorize(Roles = "Student,Teacher,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeachersPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _teacherService.GetPaginatedTeachersAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Student,Teacher,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudent(int id)
        {
<<<<<<< HEAD
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            return teacher is null ? NotFound($"Teacher with id = {id} not found") : Ok(teacher);
=======
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
            var newTeacher = new Teacher(teacher.FirstName, teacher.LastName, teacher.MiddleName, teacher.Suffix, teacher.Email, teacher.PhoneNumber, teacher.Department);
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
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
        }

        [HttpPut("update/{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeacher(int id, UpdateTeacherDto teacherDto)
        {
            if (id != teacherDto.Id)
                return BadRequest("Teacher ID mismatch");

            try
            {
                await _teacherService.UpdateTeacherAsync(id, teacherDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
<<<<<<< HEAD
=======
            teacherToUpdate.Update(teacher.ID, teacher.Department, teacher.Email, teacher.FirstName, teacher.LastName, teacher.MiddleName, teacher.Suffix, teacher.PhoneNumber);
            _context.Entry(teacherToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
        }

        [HttpPatch("update-selective/{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeacherSelectively(int id, [FromBody] JsonPatchDocument<UpdateTeacherDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Patch document cannot be null");

            var existingTeacher = await _teacherService.GetTeacherByIdAsync(id);
            if (existingTeacher == null)
                return NotFound($"Teacher with id = {id} not found");

            var dtoToPatch = new UpdateTeacherDto(existingTeacher.ID);
            patchDoc.ApplyTo(dtoToPatch, ModelState);

            if (!TryValidateModel(dtoToPatch))
                return ValidationProblem(ModelState);

            try
            {
                await _teacherService.UpdateTeacherSelectivelyAsync(id, dtoToPatch);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
<<<<<<< HEAD
=======

            teacher.Update(mapTeacherDto.ID, mapTeacherDto.Department, mapTeacherDto.Email, mapTeacherDto.FirstName, mapTeacherDto.LastName, mapTeacherDto.MiddleName, mapTeacherDto.Suffix, mapTeacherDto.PhoneNumber);
            await _context.SaveChangesAsync();

            return NoContent();
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                await _teacherService.DeleteTeacherAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}