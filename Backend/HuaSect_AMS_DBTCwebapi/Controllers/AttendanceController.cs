using HuaSect_AMS_DBTC.Service;
using HuaSect_AMS_DBTCclasslib;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService) => _attendanceService = attendanceService;

        [HttpGet]
        public async Task<IActionResult> GetAttendanceRecords()
        {
            var attendanceRecords = await _attendanceService.GetAllAttendanceRecordsAsync();
            return Ok(attendanceRecords);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAttendanceRecordsPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _attendanceService.GetPaginatedAttendanceRecordsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCourse(int id)
        {
            var attendanceRecord = await _attendanceService.GetAttendanceRecordByIdAsync(id);
            return attendanceRecord == null ? NotFound($"AttendanceRecord with id = {id} not found") : Ok(attendanceRecord);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAttendanceRecord(CreateAttendanceRecordDto attendanceRecordDto)
        {
            var createdDto = await _attendanceService.CreateAttendanceRecordAsync(attendanceRecordDto);
            // Note: Changed to nameof(GetCourse) to return a valid Location header
            return CreatedAtAction(nameof(CreateAttendanceRecord), new { id = createdDto.ID }, createdDto);
        }

        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCourse(int id, UpdateAttendanceRecordDto courseDto)
        {
            if (id != courseDto.ID)
                return BadRequest("Course ID mismatch");

            try
            {
                await _attendanceService.UpdateAttendanceRecordAsync(id, courseDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("update-selectively/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCourseSelectively(int id, [FromBody] JsonPatchDocument<UpdateAttendanceRecordDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Patch document cannot be null");

            var existingAttendanceRecord = await _attendanceService.GetAttendanceRecordByIdAsync(id);
            if (existingAttendanceRecord == null)
                return NotFound($"Attendance Record with id = {id} not found");

            var dtoToPatch = new UpdateAttendanceRecordDto { ID = existingAttendanceRecord.ID };
            patchDoc.ApplyTo(dtoToPatch, ModelState);

            if (!TryValidateModel(dtoToPatch))
                return ValidationProblem(ModelState);

            try
            {
                await _attendanceService.UpdateAttendanceRecordSelectivelyAsync(id, dtoToPatch);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                await _attendanceService.DeleteAttendanceRecordAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}