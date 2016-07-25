using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GigHub.Controllers
{
    [Route("api/AttendancesTest")]
    [Authorize]
    public class AttendancesTestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendancesTestController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/AttendancesTest
        [HttpGet]
        public async Task<IEnumerable<Attendance>> GetAttendances()
        {
            var userId = await GetCurrentUserAsync();
            return _context.Attendances;
        }

        // GET: api/AttendancesTest/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Attendance attendance = await _context.Attendances.SingleOrDefaultAsync(m => m.GigId == id);

            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        // PUT: api/AttendancesTest/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance([FromRoute] int id, [FromBody] Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != attendance.GigId)
            {
                return BadRequest();
            }

            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        [HttpPost]
        public async Task<IActionResult> PostAttendance([FromBody] int gigId)
        {
            var userId = (await GetCurrentUserAsync()).Id;

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gigId))
                return BadRequest("The attendance already exists");

            var attendance = new Attendance
            {
                GigId = gigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return Ok(attendance);
        }
        //// POST: api/AttendancesTest
        //[HttpPost]
        //public async Task<IActionResult> PostAttendance([FromBody] Attendance attendance)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Attendances.Add(attendance);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (AttendanceExists(attendance.GigId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetAttendance", new { id = attendance.GigId }, attendance);
        //}

        // DELETE: api/AttendancesTest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Attendance attendance = await _context.Attendances.SingleOrDefaultAsync(m => m.GigId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return Ok(attendance);
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.GigId == id);
        }
    }
}