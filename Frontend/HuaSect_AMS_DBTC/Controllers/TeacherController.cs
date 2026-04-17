using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourApp.Models;
using YourApp.Services; // ITeacherService — wire up your own data service

namespace YourApp.Controllers
{
    [Authorize(Roles = "Teacher,Admin")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        // ── Constructor injection ──────────────────────────────────
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // ── Helpers ───────────────────────────────────────────────

        /// <summary>
        /// Resolves the currently authenticated teacher's Id from claims.
        /// Adjust the claim type to match your auth setup.
        /// </summary>
        private int CurrentTeacherId()
        {
            var idClaim = User.FindFirst("TeacherId")?.Value
                       ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            return int.TryParse(idClaim, out var id) ? id : 0;
        }

        // ── Actions ───────────────────────────────────────────────

        /// <summary>GET /Teacher/Dashboard — course list + quick stats.</summary>
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var teacherId = CurrentTeacherId();
            var vm = await _teacherService.GetDashboardAsync(teacherId);

            if (vm is null) return NotFound();

            ViewData["TeacherName"] = vm.Teacher.FullName;
            ViewData["ActiveNav"] = "home";
            ViewData["Title"] = "Dashboard";

            return View("TeacherView", vm);
        }

        /// <summary>GET /Teacher/Courses — alias of Dashboard, nav highlight = courses.</summary>
        [HttpGet]
        public async Task<IActionResult> Courses()
        {
            var teacherId = CurrentTeacherId();
            var vm = await _teacherService.GetDashboardAsync(teacherId);

            if (vm is null) return NotFound();

            ViewData["TeacherName"] = vm.Teacher.FullName;
            ViewData["ActiveNav"] = "courses";
            ViewData["Title"] = "My Courses";

            return View("TeacherView", vm);
        }

        /// <summary>GET /Teacher/Attendance/{id} — attendance history for one course.</summary>
        [HttpGet]
        public async Task<IActionResult> Attendance(int id)
        {
            var teacherId = CurrentTeacherId();
            var vm = await _teacherService.GetAttendanceAsync(teacherId, id);

            if (vm is null) return NotFound();

            ViewData["TeacherName"] = (await _teacherService.GetTeacherAsync(teacherId))?.FullName ?? "Teacher";
            ViewData["ActiveNav"] = "attendance";
            ViewData["Title"] = $"Attendance — {vm.Course.Code}";

            return View("AttendanceView", vm);
        }

        /// <summary>GET /Teacher/Reports.</summary>
        [HttpGet]
        public async Task<IActionResult> Reports()
        {
            var teacherId = CurrentTeacherId();
            var teacher = await _teacherService.GetTeacherAsync(teacherId);

            ViewData["TeacherName"] = teacher?.FullName ?? "Teacher";
            ViewData["ActiveNav"] = "reports";
            ViewData["Title"] = "Reports";

            return View();
        }

        /// <summary>GET /Teacher/Settings.</summary>
        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var teacherId = CurrentTeacherId();
            var teacher = await _teacherService.GetTeacherAsync(teacherId);

            ViewData["TeacherName"] = teacher?.FullName ?? "Teacher";
            ViewData["ActiveNav"] = "settings";
            ViewData["Title"] = "Settings";

            return View();
        }
    }
}

// ─────────────────────────────────────────────────────────────────────────────
// ITeacherService — define your data-access contract here
// (implement with EF Core, Dapper, or any repository you prefer)
// ─────────────────────────────────────────────────────────────────────────────
namespace YourApp.Services
{
    using YourApp.Models;

    public interface ITeacherService
    {
        Task<Teacher?> GetTeacherAsync(int teacherId);
        Task<TeacherDashboardViewModel?> GetDashboardAsync(int teacherId);
        Task<AttendanceHistoryViewModel?> GetAttendanceAsync(int teacherId, int courseId);
    }

    // ── Sample in-memory stub (replace with real DB calls) ───────────────────
    public class TeacherServiceStub : ITeacherService
    {
        private static readonly List<Teacher> _teachers = new()
        {
            new Teacher { Id = 1, FirstName = "Dench", LastName = "Mery", Email = "dmery@edu.ph" }
        };

        private static readonly List<Course> _courses = new()
        {
            new Course { Id = 1, Code = "IT 210A", Title = "Information Management",        Days = "Mon & Sat", TimeSlot = "8:00 AM – 9:00 AM",   TeacherId = 1, StudentCount = 35 },
            new Course { Id = 2, Code = "GEC 210A",Title = "Readings in Philippine History", Days = "Mon & Sat", TimeSlot = "9:00 AM – 12:00 PM",  TeacherId = 1, StudentCount = 40 },
            new Course { Id = 3, Code = "IT 211B", Title = "Web Applications Development",  Days = "Mon & Sat", TimeSlot = "1:00 PM – 3:00 PM",   TeacherId = 1, StudentCount = 30 },
        };

        public Task<Teacher?> GetTeacherAsync(int teacherId)
            => Task.FromResult(_teachers.FirstOrDefault(t => t.Id == teacherId));

        public Task<TeacherDashboardViewModel?> GetDashboardAsync(int teacherId)
        {
            var teacher = _teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher is null) return Task.FromResult<TeacherDashboardViewModel?>(null);

            var vm = new TeacherDashboardViewModel
            {
                Teacher = teacher,
                CoursesToday = 2,
                Courses = _courses
                    .Where(c => c.TeacherId == teacherId)
                    .Select(c => new CourseCardViewModel
                    {
                        CourseId = c.Id,
                        Title = c.Title,
                        Code = c.Code,
                        FacultyName = teacher.FullName,
                        Days = c.Days,
                        TimeSlot = c.TimeSlot,
                        StudentCount = c.StudentCount,
                    })
                    .ToList()
            };

            return Task.FromResult<TeacherDashboardViewModel?>(vm);
        }

        public Task<AttendanceHistoryViewModel?> GetAttendanceAsync(int teacherId, int courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId && c.TeacherId == teacherId);
            if (course is null) return Task.FromResult<AttendanceHistoryViewModel?>(null);

            var teacher = _teachers.First(t => t.Id == teacherId);
            var vm = new AttendanceHistoryViewModel
            {
                Course = new CourseCardViewModel
                {
                    CourseId = course.Id,
                    Title = course.Title,
                    Code = course.Code,
                    FacultyName = teacher.FullName,
                    Days = course.Days,
                    TimeSlot = course.TimeSlot,
                },
                Records = new List<AttendanceRecord>
                {
                    new() { Id=1, CourseId=courseId, SessionDate=DateTime.Today.AddDays(-7),  Present=30, Absent=3, Late=2 },
                    new() { Id=2, CourseId=courseId, SessionDate=DateTime.Today.AddDays(-14), Present=28, Absent=5, Late=2 },
                }
            };

            return Task.FromResult<AttendanceHistoryViewModel?>(vm);
        }
    }
}
