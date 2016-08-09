using System.Threading.Tasks;
using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Data;
using GigHub.Persistance.Repositories;

namespace GigHub.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IAttendanceRepository Attendances { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IGigRepository Gigs { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Attendances = new AttendanceRepository(context);
            Genres = new GenreRepository(context);
            Gigs = new GigRepository(context);
            Followings = new FollowingRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
