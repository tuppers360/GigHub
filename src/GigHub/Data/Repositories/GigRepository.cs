using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Models;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Data.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances)
                .ThenInclude(a => a.Attendee)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public async Task<Gig> GetGigWithAttendeesAsync(int gigId)
        {
            return await _context.Gigs
                .Include(g => g.Attendances)
                .ThenInclude(a => a.Attendee)
                .SingleOrDefaultAsync(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            //TODO: Will require review when EF updates API for Include and tree design for Includes
            return _context.Attendances.Where(a => a.AttendeeId == userId)
                .Include(g => g.Gig)
                .ThenInclude(a => a.Artist)
                .Include(g => g.Gig)
                .ThenInclude(g => g.Genre)
                .ToList()
                .Select(a => a.Gig);
        }

        //public async Task<IEnumerable<Gig>> GetGigsUserAttendingAsync(string userId)
        //{
        //    //TODO: Will require review when EF updates API for Include and tree design for Includes
        //    return await _context.Attendances.Where(a => a.AttendeeId == userId)
        //        .Include(g => g.Gig)
        //        .ThenInclude(a => a.Artist)
        //        .Include(g => g.Gig)
        //        .ThenInclude(g => g.Genre)
        //        .ToListAsync();
        //        .Select(a => a.Gig);
        //}

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && !g.IsCancelled)
                .Include(g => g.Genre)
                .ToList();
        }

        public async Task<IEnumerable<Gig>> GetUpcomingGigsByArtistAsync(string artistId)
        {
            return await _context.Gigs
                .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && !g.IsCancelled)
                .Include(g => g.Genre)
                .ToListAsync();
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}
