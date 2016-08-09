using System.Collections.Generic;
using System.Threading.Tasks;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGig(int gigId);
        Gig GetGigWithAttendees(int gigId);
        Task<Gig> GetGigWithAttendeesAsync(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId);
        Task<IEnumerable<Gig>> GetUpcomingGigsByArtistAsync(string artistId);
        void Add(Gig gig);
    }
}