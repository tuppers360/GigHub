using System.Threading.Tasks;
using GigHub.Data.Repositories;

namespace GigHub.Persistance
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        IFollowingRepository Followings { get; }
        void Complete();
        Task CompleteAsync();
    }
}