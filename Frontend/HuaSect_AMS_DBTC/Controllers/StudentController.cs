using HuaSect_AMS_DBTC.Models;
using HuaSect_AMS_DBTC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    [Authorize("Student")]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IAttendanceService _attendanceService;

        public StudentController(ICourseService courseService, IAttendanceService attendanceService)
        {
            _courseService = courseService;
            _attendanceService = attendanceService;
        }

        [HttpGet("student-dashboard")]
        public async Task<IActionResult> StudentDashboard()
        {
            ICollection<Course> courses;
            try
            {
                courses = await _courseService.GetAllCoursesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var studentDashboardModel = new StudentDashboardPage
            {
                Courses = courses,
            };
            return View("StudentDashboard", studentDashboardModel);
        }

        [HttpGet("attendance-profile")]
        public async Task<IActionResult> StudentList([FromQuery] int courseId, [FromQuery] int studentId)
        {
            Course? course;
            ICollection<Attendance> attendanceRecords;
            try
            {
                course = await _courseService.GetCourseByIdAsync(courseId);
                attendanceRecords = await _attendanceService.GetStudentAttendanceRecordsAsync(studentId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var attendanceProfileModel = new AttendanceProfilePage
            {
                Course = course,
                AttendanceRecords = attendanceRecords
            };
            return View("AttendanceProfile", attendanceProfileModel);
        }

        [HttpGet("settings")]
        public async Task<IActionResult> Settings()
        {
            return View("Settings");
        }
    }
}