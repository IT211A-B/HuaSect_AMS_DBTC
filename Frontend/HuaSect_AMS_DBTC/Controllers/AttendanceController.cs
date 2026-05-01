using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View("AttendanceView");
        }
    }
}