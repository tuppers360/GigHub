using System.Linq;
using GigHub.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GigHub.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Gig>()
                .HasOne(g => g.Genre)
                .WithMany()
                .HasForeignKey(g => g.GenreId);

            builder.Entity<Gig>()
                .HasOne(g => g.Artist)
                .WithMany()
                .HasForeignKey(g => g.ArtistId);

            builder.Entity<Attendance>()
                .HasKey(a => new { a.GigId, a.AttendeeId });

            builder.Entity<Attendance>()
                .HasOne(a => a.Gig)
                .WithMany(g => g.Attendances)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserNotification>()
                .HasKey(n => new { n.UserId, n.NotificationId });

            builder.Entity<UserNotification>()
                .HasOne(n => n.User)
                .WithMany(u=>u.UserNotifications)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserNotification>()
                .HasOne(n => n.Notification)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Following>()
                .HasKey(f => new { f.FollowerId, f.FolloweeId });

            builder.Entity<Following>()
                .HasOne(e => e.Followee)
                .WithMany(f => f.Followers)
                .HasForeignKey(e=>e.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Following>()
                .HasOne(f => f.Follower)
                .WithMany(e => e.Followees)
                .HasForeignKey(f=>f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
