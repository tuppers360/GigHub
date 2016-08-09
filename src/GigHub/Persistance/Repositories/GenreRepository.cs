using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Data;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Persistance.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}
