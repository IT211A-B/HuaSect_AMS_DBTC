using System.Threading.Tasks;
using HuaSect_AMS_DBTC.Models;
using HuaSect_AMS_DBTC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    public class LogInController : Controller
    {
        private readonly IAuthService _authService;

        public LogInController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("LogInView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInModel model)
        {
            if (!ModelState.IsValid)
                return View(model); // Re-renders with validation errors
            
            List<string> cookies;

            try
            {
                cookies = (await _authService.LoginAsync(model)).ToList();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            foreach (var cookie in cookies)
            {
                // 3. Forward the cookie to the User's Browser
                Response.Headers.Append("Set-Cookie", cookie);
            }
            if (model.Role == "student")
            {
                return RedirectToAction("StudentDashboard", "Student");
            } else
            {
                return RedirectToAction("StudentManagement", "Teacher");
            }
        }
    }
}