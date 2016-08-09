using System.Collections.Generic;
using System.Threading.Tasks;
using GigHub.Models;

namespace GigHub.Data.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
        Task<IEnumerable<Genre>> GetGenresAsync();
    }
}