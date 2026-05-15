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
        public AdminController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("user-management")]
        public IActionResult UserManagement()
        {
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
            // _courseService.CreateCourse();
            return RedirectToAction("UserManagement");
        }
    }
}