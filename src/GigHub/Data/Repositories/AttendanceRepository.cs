using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Models;

namespace GigHub.Data.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        //public bool GetAttendance(int gigId, string userId)
        //{
        //    return _context.Attendances
        //        .SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
        //}
        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }
    }
}
