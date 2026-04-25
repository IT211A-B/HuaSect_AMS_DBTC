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
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            return teacher is null ? NotFound($"Teacher with id = {id} not found") : Ok(teacher);
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