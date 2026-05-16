using System.Threading.Tasks;
using HuaSect_AMS_DBTC.Models;
using HuaSect_AMS_DBTC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    // [Authorize("Admin")]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        public AdminController(ICourseService courseService, IStudentService studentService, ITeacherService teacherService)
        {
            _courseService = courseService;
            _studentService = studentService;
            _teacherService = teacherService;
        }

        [HttpGet("user-management")]
        public async Task<IActionResult> UserManagement()
        {
            var userCookie = Request.Cookies["AuthToken"];
            ICollection<Student> students;
            ICollection<Teacher> teachers;
            try
            {
               students = await _studentService.GetAllStudentsAsync(userCookie);
               teachers = await _teacherService.GetAllTeachersAsync(userCookie);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var userManagementPage = new UserManagementPage
            {
                Students = students,
                Teachers = teachers,
            };
            return View("UserManagementView");
        }

        [HttpPost("user-management")]
        public IActionResult UserManagementPost()
        {
            return View("UserManagementView");
        }

        [HttpGet("create-course")]
        public IActionResult CreateCourse()
        {
            return View("CreateCoure");
        }

        [HttpPost("create-course")]
        public IActionResult CreateCoursePost([FromBody] CreateCourseModel model)
        {
            var userCookie = Request.Cookies["AuthToken"];
            _courseService.CreateCourse(model, userCookie);
            return RedirectToAction("UserManagement");
        }
    }
}