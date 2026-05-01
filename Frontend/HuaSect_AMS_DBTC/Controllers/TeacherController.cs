using HuaSect_AMS_DBTC.Models;
using HuaSect_AMS_DBTC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    //[Authorize(Roles = "Teacher,Admin")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        private int CurrentTeacherId()
        {
            var claim = User.FindFirst("TeacherId")?.Value
                     ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(claim, out var id) ? id : 0;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var vm = await _teacherService.GetDashboardAsync(CurrentTeacherId());
            if (vm is null) return NotFound();
            ViewData["TeacherName"] = vm.Teacher.FullName;
            ViewData["ActiveNav"] = "home";
            ViewData["Title"] = "Dashboard";
            return View("TeacherView", vm);
        }

        [HttpGet]
        public async Task<IActionResult> Courses()
        {
            var vm = await _teacherService.GetDashboardAsync(CurrentTeacherId());
            if (vm is null) return NotFound();
            ViewData["TeacherName"] = vm.Teacher.FullName;
            ViewData["ActiveNav"] = "courses";
            ViewData["Title"] = "My Courses";
            return View("TeacherView", vm);
        }

        [HttpGet]
        public async Task<IActionResult> Attendance(int id)
        {
            var vm = await _teacherService.GetAttendanceAsync(CurrentTeacherId(), id);
            if (vm is null) return NotFound();
            ViewData["ActiveNav"] = "attendance";
            return View("AttendanceView", vm);
        }

        [HttpGet]
        public IActionResult Reports()
        {
            ViewData["ActiveNav"] = "reports";
            return View();
        }

        [HttpGet]
        public IActionResult Settings()
        {
            ViewData["ActiveNav"] = "settings";
            return View();
        }
    }
}