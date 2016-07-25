using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Models
{
    public class Attendance
    {
        public Gig Gig { get; set; }

        public ApplicationUser Attendee { get; set; }

        public int GigId { get; set; }

        public string AttendeeId { get; set; }
    }
}
