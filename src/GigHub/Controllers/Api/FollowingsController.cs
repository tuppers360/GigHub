using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GigHub.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Followings")]
    [Authorize]
    public class FollowingsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public FollowingsController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Follow(FollowingDto dto)
        {
            var userId = (await GetCurrentUserAsync()).Id;

            if (_context.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("Following Already Exists");

            var following = new Following
            {
                FolloweeId = userId,
                FollowerId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }

    public class FollowingDto
    {
        public string FolloweeId { get; set; }
    }
}