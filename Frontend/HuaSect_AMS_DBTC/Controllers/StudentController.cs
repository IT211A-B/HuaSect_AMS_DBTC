using HuaSect_AMS_DBTC.Models;
using HuaSect_AMS_DBTC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("student-dashboard")]
        public async Task<IActionResult> StudentDashboard()
        {
            return View("StudentDashboard");
        }

        [HttpGet("attendance-profile")]
        public async Task<IActionResult> StudentList()
        {
            return View("AttendanceProfile");
        }

        [HttpGet("settings")]
        public async Task<IActionResult> Settings()
        {
            return View("Settings");
        }
    }
}