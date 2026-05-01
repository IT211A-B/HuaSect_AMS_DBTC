using HuaSect_AMS_DBTC.Models;
using HuaSect_AMS_DBTC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            var model = new StudentViewModel { Students = students.ToList() };
            return View("StudentView", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent(StudentInputModel input)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in all required fields.";
                return RedirectToAction(nameof(Index));
            }

            var student = new StudentModel
            {
                FirstName = input.FirstName.Trim(),
                MiddleName = input.MiddleName?.Trim(),
                LastName = input.LastName.Trim(),
                StudentNo = input.StudentNo.Trim(),
                YearLevel = input.YearLevel.Trim(),
                Email = input.Email.Trim().ToLower(),
                CourseId = input.CourseId.Trim(),
            };

            await _studentService.AddStudentAsync(student);
            TempData["Success"] = $"Student '{student.FullName}' added successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStudent(StudentInputModel input)
        {
            if (!ModelState.IsValid || input.Id == 0)
            {
                TempData["Error"] = "Invalid student data.";
                return RedirectToAction(nameof(Index));
            }

            var student = await _studentService.GetStudentByIdAsync(input.Id);
            if (student == null)
            {
                TempData["Error"] = "Student not found.";
                return RedirectToAction(nameof(Index));
            }

            student.FirstName = input.FirstName.Trim();
            student.MiddleName = input.MiddleName?.Trim();
            student.LastName = input.LastName.Trim();
            student.StudentNo = input.StudentNo.Trim();
            student.YearLevel = input.YearLevel.Trim();
            student.Email = input.Email.Trim().ToLower();
            student.CourseId = input.CourseId.Trim();

            await _studentService.UpdateStudentAsync(student);
            TempData["Success"] = $"Student '{student.FullName}' updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                TempData["Error"] = "Student not found.";
                return RedirectToAction(nameof(Index));
            }

            await _studentService.DeleteStudentAsync(id);
            TempData["Success"] = "Student deleted.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Attendance(int id)
        {
            var records = await _studentService.GetAttendanceByStudentIdAsync(id);
            if (records == null) return NotFound();
            return Json(records);
        }

        [HttpGet]
        public async Task<IActionResult> StudentList(string course, DateTime? date)
        {
            var model = await _studentService.GetStudentListViewModelAsync(course, date ?? DateTime.Today);
            return View("StudentList", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAttendance(List<AttendanceInputModel> records)
        {
            if (records == null || records.Count == 0)
            {
                TempData["Error"] = "No attendance records to save.";
                return RedirectToAction(nameof(StudentList));
            }

            await _studentService.SaveAttendanceAsync(records);
            TempData["Success"] = "Attendance saved.";
            return RedirectToAction(nameof(StudentList));
        }
    }
}