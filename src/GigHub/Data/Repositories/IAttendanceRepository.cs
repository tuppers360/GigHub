using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Data.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}