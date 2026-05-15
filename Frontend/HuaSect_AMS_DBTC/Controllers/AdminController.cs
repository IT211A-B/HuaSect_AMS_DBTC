using System.Threading.Tasks;
using HuaSect_AMS_DBTC.Models;
using HuaSect_AMS_DBTC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HuaSect_AMS_DBTC.Controllers
{
    [Authorize("Admin")]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        public AdminController(IAuthService authService)
        {
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("LogInView");
        }
    }
}