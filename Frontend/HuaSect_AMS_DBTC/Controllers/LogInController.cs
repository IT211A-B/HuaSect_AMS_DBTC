using AttendanceChecking.Models;
using Microsoft.AspNetCore.Mvc;


namespace HuaSect_AMS_DBTC.Controllers
{
    public class LogInController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LogInModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle logic based on model.Role
                return RedirectToAction("Dashboard");
            }
            return View("Index", model);
        }
    }
}
