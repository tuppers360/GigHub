using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GigHub.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendancesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> PostAttendance(AttendanceDto dto)
        {
            var user = await GetCurrentUserAsync();

            if (_context.Attendances.Any(a => a.AttendeeId == user.Id && a.GigId == dto.GigId))
                return BadRequest("The attendance already exists");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = user.Id
            };

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return Ok(attendance);
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}