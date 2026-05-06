using HuaSect_AMS_DBTC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    public class LogInController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("LogInView");
        }

        [HttpPost]
        public IActionResult Login(LogInModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Dashboard");
            }
            return View("Index", model);
        }
    }
}