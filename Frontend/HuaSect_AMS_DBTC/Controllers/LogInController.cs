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
    }
}