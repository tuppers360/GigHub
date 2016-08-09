using System.Linq;
using System.Threading.Tasks;
using GigHub.Core.Models;
using GigHub.Data;
using GigHub.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Gigs")]
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GigsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = (await GetCurrentUserAsync()).Id;

            var gig = _context.Gigs
                .Include(g => g.Attendances)
                .ThenInclude(a => a.Attendee)
                .Include(a=>a.Attendances)
                .ThenInclude(a=>a.Gig)
                .ToList()
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancelled)
                return NotFound();

            gig.IsCancelled = true;

            var notification = Notification.GigCanceled(gig);

            foreach (var attendee in gig.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}