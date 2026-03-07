using HuaSect_AMS_DBTCclasslib.DbCtx;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DatabaseCtx _context;
        public CourseController(DatabaseCtx context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await _context.Course.ToListAsync());
        }
    }
}
