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
        public IActionResult Login(LogInModel model)
        {
            if (!ModelState.IsValid)
                return View(model); // Re-renders with validation errors
            
            _authService.LoginAsync(model);

            return RedirectToAction("Dashboard");
        }
    }
}