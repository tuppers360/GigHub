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
