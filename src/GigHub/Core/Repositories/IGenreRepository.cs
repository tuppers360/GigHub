using System.Collections.Generic;
using System.Threading.Tasks;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
        Task<IEnumerable<Genre>> GetGenresAsync();
    }
}