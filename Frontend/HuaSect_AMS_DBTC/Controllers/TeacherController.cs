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

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("student-management")]
        public async Task<IActionResult> StudentManagement()
        {
            return View("StudentManagement");
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