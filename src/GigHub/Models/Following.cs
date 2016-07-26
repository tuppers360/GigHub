using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Models
{
    public class Following
    {
        public string FolloweeId { get; set; }
        public ApplicationUser Followee { get; set; }

        // Navigation
        public string FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }
    }
}
