using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Data;
using GigHub.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationsController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<NotificationDto>> GetNewNotifications()
        {
            var userId = (await GetCurrentUserAsync()).Id;

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Include(u=>u.Notification).ThenInclude(u=>u.Gig).ThenInclude(u=>u.Artist)
                .ToList()
                .Select(un => un.Notification);


            //return notifications.Select(Mapper.Map<Notification, NotificationDto>);

            return notifications.Select(n => new NotificationDto()
            {
                DateTime = n.DateTime,
                Gig = new GigDto()
                {
                    Artist = new UserDto()
                    {
                        Id = n.Gig.Artist.Id,
                        Name = n.Gig.Artist.Name
                    },
                    DateTime = n.Gig.DateTime,
                    Id = n.Gig.Id,
                    IsCancelled = n.Gig.IsCancelled,
                    Venue = n.Gig.Venue
                },
                OriginalDateTime = n.OriginalDateTime,
                OriginalVenue = n.OriginalVenue,
                NotificationType = n.NotificationType
            });
        }

        [HttpPost]
        [Route("MarkAsRead")]
        public async Task<IActionResult> MarkAsRead()
        {
            var userId = (await GetCurrentUserAsync()).Id;

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            notifications.ForEach(n=>n.Read());

            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}