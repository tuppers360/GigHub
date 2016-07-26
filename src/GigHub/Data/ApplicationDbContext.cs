using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GigHub.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

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

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            
            builder.Entity<Attendance>()
                .HasKey(a => new { a.GigId, a.AttendeeId });

            builder.Entity<Attendance>()
                .HasOne(a => a.Gig)
                .WithMany();

            builder.Entity<Following>()
                .HasKey(f => new { f.FollowerId, f.FolloweeId });

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithOne(f => f.Followee);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithOne(f => f.Follower);
        }
    }
}
