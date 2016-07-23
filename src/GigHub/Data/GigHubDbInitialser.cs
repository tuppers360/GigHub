using System;
using System.Linq;
using GigHub.Models;

namespace GigHub.Data
{
    public class GigHubDbInitialser
    {
        private static ApplicationDbContext _context;

        public static void Initialise(IServiceProvider serviceProvider)
        {
            _context = (ApplicationDbContext) serviceProvider.GetService(typeof(ApplicationDbContext));
            InitialiseGenre();
        }

        private static void InitialiseGenre()
        {
            if (_context.Genres.Any()) return;

            _context.Genres.AddRange(
                new Genre {Name = "Jazz"},
                new Genre {Name = "Blues"},
                new Genre {Name = "Rock"},
                new Genre {Name = "Country"}
            );

            _context.SaveChanges();
        }
    }
}
