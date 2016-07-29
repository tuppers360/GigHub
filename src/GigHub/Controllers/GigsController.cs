using System;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models.GigViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Mine()
        {
            var userId = (await GetCurrentUserAsync()).Id;
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCancelled)
                .Include(g=>g.Genre)
                .ToList();

            return View(gigs);
        }
        public async Task<IActionResult> Attending()
        {
            var userId = (await GetCurrentUserAsync()).Id;

            //TODO: Will require review when EF updates API for Include and tree design for Includes
            var gigs =
                _context.Attendances.Where(a => a.AttendeeId == userId)
                    .Include(g => g.Gig)
                    .ThenInclude(a => a.Artist)
                    .Include(g => g.Gig)
                    .ThenInclude(g => g.Genre)
                    .ToList()
                    .Select(a => a.Gig);

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };

            return View("Gigs", viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = (await GetCurrentUserAsync()).Id;
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel()
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = (await GetCurrentUserAsync()).Id,
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            gig.Created();

            _context.Gigs.Add(gig);
            await _context.SaveChangesAsync();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = (await GetCurrentUserAsync()).Id;
            var gig = _context.Gigs
                .Include(g=>g.Attendances)
                .ThenInclude(a=>a.Attendee)
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            await _context.SaveChangesAsync();

            return RedirectToAction("Mine", "Gigs");
        }
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}