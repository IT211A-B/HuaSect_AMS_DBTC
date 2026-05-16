using System.Threading.Tasks;
using HuaSect_AMS_DBTC.Models;
using HuaSect_AMS_DBTC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("LogInView");
        }

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInModel model)
        {
            // todo need to check if user email confirmed
            if (!ModelState.IsValid)
                return View(model); // Re-renders with validation errors

            LogInResponseModel token;

            try
            {
                token = await _authService.LoginAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            Response.Cookies.Append("AuthToken", token.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromMinutes(15),
                Path="/"
            });
            if (model.Role == "student")
            {
                return RedirectToAction("StudentDashboard", "Student");
            }
            else
            {
                return RedirectToAction("StudentManagement", "Teacher");
            }
        }

        [HttpGet("register-student")]
        public IActionResult RegisterStudent()
        {
            return View("RegisterStudentView");
        }

        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterStudentPost([FromBody] RegisterStudentModel model)
        {
            await _authService.RegisterStudentAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet("register-teacher")]
        public IActionResult RegisterTeacher()
        {
            return View("RegisterTeacherView");
        }

        [HttpPost("register-teacher")]
        public async Task<IActionResult> RegisterTeacherPost([FromBody] RegisterTeacherModel model)
        {
            await _authService.RegisterTeacherAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet("confirm-email")]
        public IActionResult ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            _authService.ConfirmEmailAsync(userId, token);
            return RedirectToAction("Login");
        }
    }
}