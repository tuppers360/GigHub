using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancelled)
                return NotFound();

            gig.IsCancelled = true;

            await _context.SaveChangesAsync();

            return Ok();
        }
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}