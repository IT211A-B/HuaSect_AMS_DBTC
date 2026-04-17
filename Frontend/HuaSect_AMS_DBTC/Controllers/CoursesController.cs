using AttendanceSystem.Models;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Controllers
{
    /// <summary>
    /// Handles all Course List and Attendance management routes.
    /// </summary>
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

        // ── GET /Courses ──────────────────────────────────────────────────

        /// <summary>
        /// Displays the Course List / Attendance main page.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(string courseId = null, DateTime? date = null)
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

            // Pre-load existing attendance records for the chosen date
            if (!string.IsNullOrEmpty(courseId))
            {
                var records = await _attendanceService.GetRecordsAsync(courseId, selectedDate);
                model.AttendanceRecords = records.ToDictionary(r => r.StudentId, r => r.Status);
            }

            return View("~/Views/Courses/CoursesView.cshtml", model);
        }

        // ── GET /Courses/Details/{id} ─────────────────────────────────────

        /// <summary>
        /// Returns details for a single course (used by AJAX / partial refresh).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            return Json(new
            {
                course.Id,
                course.Name,
                course.Code,
                course.Description,
            });
        }

        // ── POST /Courses/SaveAttendance ──────────────────────────────────

        /// <summary>
        /// Receives and persists the attendance payload from the client.
        /// </summary>
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

                return Ok(new { success = true, message = "Attendance saved successfully.", count = records.Count });
            }
            catch (Exception ex)
            {
                // Log ex in production
                return StatusCode(500, new { success = false, message = "An error occurred while saving." });
            }
        }

        // ── POST /Courses/Create ──────────────────────────────────────────

        /// <summary>
        /// Creates a new course.
        /// </summary>
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

        // ── GET /Courses/Back ─────────────────────────────────────────────

        /// <summary>
        /// Navigates back; falls back to Index if no referrer.
        /// </summary>
        [HttpGet]
        public IActionResult Back()
        {
            var referrer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referrer))
                return Redirect(referrer);

            return RedirectToAction(nameof(Index));
        }

        // ── AJAX: /Courses/FilterStudents ─────────────────────────────────

        /// <summary>
        /// Returns students filtered by course (used by client-side live filter).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> FilterStudents(string courseId)
        {
            IEnumerable<StudentModel> students = string.IsNullOrEmpty(courseId)
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

        // ── Helpers ───────────────────────────────────────────────────────

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