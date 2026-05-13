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

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
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
            return View("CourseList");
        }

        [HttpGet("edit-student")]
        public async Task<IActionResult> EditStudent(int id)
        {
            return View("EditStudent");
        }

        [HttpGet("attendance-tracker")]
        public IActionResult AttendanceTracker()
        {
            return View("AttendanceTracker");
        }
    }
}