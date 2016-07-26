using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GigHub.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GigHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Attendance>()
                .HasOne(a => a.Gig)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Attendance>()
                .HasKey(a => new { a.GigId, a.AttendeeId });

            builder.Entity<Following>()
                .HasKey(f => new { f.FollowerId, f.FolloweeId });

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithOne(f => f.Followee)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithOne(f => f.Follower)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
