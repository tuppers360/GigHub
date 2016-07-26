using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Models
{
    public class Following
    {
        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }

        // Navigation
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
    }
}
