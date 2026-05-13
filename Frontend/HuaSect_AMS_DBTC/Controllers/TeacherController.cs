using HuaSect_AMS_DBTC.Models;
using HuaSect_AMS_DBTC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{

    [Route("[controller]")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IAttendanceService _attendanceService;

        public TeacherController(ITeacherService teacherService, IStudentService studentService, ICourseService courseService, IAttendanceService attendanceService)
        {
            _teacherService = teacherService;
            _studentService = studentService;
            _courseService = courseService;
            _attendanceService = attendanceService;
        }

        [HttpGet("{id:int}/student-management")]
        public async Task<IActionResult> StudentManagement(int id)
        {
            Teacher? teacher;
            ICollection<Student> students;
            try
            {
                teacher = await _teacherService.GetTeacherByIdAsync(id);
                students = await _studentService.GetAllStudentsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (teacher is null)
                return BadRequest($"Error: teacher with id = {id} can't be found");
            var studentManagementModel = new StudentManagementPage
            {
                Students = students,
                Teacher = teacher,
            };
            return View("StudentManagement", studentManagementModel);
        }

        [HttpGet("add-student")]
        public async Task<IActionResult> AddStudent()
        {
            return View("AddStudent");
        }

        [HttpGet("course-list")]
        public async Task<IActionResult> CourseList()
        {
            ICollection<Student> students;
            ICollection<Attendance> attendanceRecords;
            ICollection<Course> courses;
            try
            {
                students = await _studentService.GetAllStudentsAsync();
                attendanceRecords = await _attendanceService.GetAllAttendanceRecordsAsync();
                courses = await _courseService.GetAllCoursesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var courseListModel = new CourseListPage
            {
                Students = students,
                AttendanceRecords = attendanceRecords,
                Courses = courses,
            };
            return View("CourseList", courseListModel);
        }

        [HttpGet("edit-student")]
        public async Task<IActionResult> EditStudent()
        {
            return View("EditStudent");
        }

        [HttpGet("attendance-tracker")]
        public IActionResult AttendanceTracker([FromQuery] int courseId, [FromQuery] int studentId)
        {
            var attendanceTrackerModel = new AttendanceTrackerPage
            {
                Course = ,
                Student = ,
            };
            return View("AttendanceTracker");
        }
    }
}