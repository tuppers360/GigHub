using System.Linq;
using System.Threading.Tasks;
using GigHub.Data;
using GigHub.Models;
using GigHub.Persistance;
using GigHub.ViewModels.GigViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GigHub.Controllers
{
    [Authorize]
    public class GigsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public GigsController(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Attending()
        {
            var userId = (await GetCurrentUserAsync()).Id;

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
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

            _unitOfWork.Gigs.Add(gig);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Mine", "Gigs");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return NotFound();

            var viewModel = new GigDetailsViewModel {Gig = gig};

            if (!User.Identity.IsAuthenticated) return View("Details", viewModel);

            var userId = (await GetCurrentUserAsync()).Id;

            viewModel.IsAttending = _context.Attendances
                .Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

            viewModel.IsFollowing = _context.Followings
                .Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == userId);

            return View("Details", viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return NotFound();

            if (gig.ArtistId != (await GetCurrentUserAsync()).Id)
                return new UnauthorizedResult();

            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _unitOfWork.Genres.GetGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        public async Task<IActionResult> Mine()
        {
            var userId = (await GetCurrentUserAsync()).Id;
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(userId);

            return View(gigs);
        }

        [HttpPost]
        public IActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = await _unitOfWork.Gigs.GetGigWithAttendeesAsync(viewModel.Id);

            if (gig == null)
                return NotFound();

            if(gig.ArtistId != (await GetCurrentUserAsync()).Id)
                return new UnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Mine", "Gigs");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}