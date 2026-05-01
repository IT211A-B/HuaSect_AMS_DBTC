using HuaSect_AMS_DBTC.Models;
using HuaSect_AMS_DBTC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly IAttendanceService _attendanceService;

        public CoursesController(
            ICourseService courseService,
            IStudentService studentService,
            IAttendanceService attendanceService)
        {
            _courseService = courseService;
            _studentService = studentService;
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? courseId = null, DateTime? date = null)
        {
            var selectedDate = date ?? DateTime.Today;

            var model = new CourseListViewModel
            {
                AvailableCourses = await _courseService.GetAllCoursesAsync(),
                SelectedCourseId = courseId,
                SelectedDate = selectedDate,
                Students = string.IsNullOrEmpty(courseId)
                    ? await _studentService.GetAllStudentsAsync()
                    : await _studentService.GetStudentsByCourseAsync(courseId),
            };

            if (!string.IsNullOrEmpty(courseId))
            {
                var records = await _attendanceService.GetRecordsAsync(courseId, selectedDate);
                model.AttendanceRecords = records
                    .ToDictionary(r => r.StudentId, r => r.Status);
            }

            return View("~/Views/Courses/CoursesView.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            return Json(new { course.Id, course.Name, course.Code, course.Description });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAttendance([FromBody] AttendanceSaveRequest request)
        {
            if (request == null || request.Attendance == null)
                return BadRequest(new { success = false, message = "Invalid payload." });

            try
            {
                var records = request.Attendance.Select(kv => new AttendanceRecord
                {
                    StudentId = kv.Key,
                    CourseId = request.CourseId,
                    Date = request.Date,
                    Status = ParseStatus(kv.Value),
                }).ToList();

                await _attendanceService.SaveRecordsAsync(records);
                return Ok(new { success = true, message = "Attendance saved.", count = records.Count });
            }
            catch
            {
                return StatusCode(500, new { success = false, message = "An error occurred." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseModel model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            await _courseService.CreateCourseAsync(model);
            TempData["SuccessMessage"] = "Course created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Back()
        {
            var referrer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referrer))
                return Redirect(referrer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> FilterStudents(string? courseId)
        {
            var students = string.IsNullOrEmpty(courseId)
                ? await _studentService.GetAllStudentsAsync()
                : await _studentService.GetStudentsByCourseAsync(courseId);

            var result = students.Select(s => new
            {
                s.Id,
                s.FullName,
                s.RollNumber,
                s.Course,
            });

            return Json(result);
        }

        private static AttendanceStatus ParseStatus(string value) =>
            value?.ToLower() switch
            {
                "present" => AttendanceStatus.Present,
                "late" => AttendanceStatus.Late,
                "absent" => AttendanceStatus.Absent,
                _ => AttendanceStatus.Unknown,
            };
    }
}