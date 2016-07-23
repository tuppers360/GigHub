using System.Linq;
using GigHub.Data;
using GigHub.Models.GigViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }
    }
}