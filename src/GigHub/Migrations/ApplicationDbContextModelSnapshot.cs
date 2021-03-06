﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using GigHub.Data;
using GigHub.Persistance;

namespace GigHub.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GigHub.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("GigHub.Models.Attendance", b =>
                {
                    b.Property<int>("GigId");

                    b.Property<string>("AttendeeId");

                    b.HasKey("GigId", "AttendeeId");

                    b.HasIndex("AttendeeId");

                    b.HasIndex("GigId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("GigHub.Models.Following", b =>
                {
                    b.Property<string>("FollowerId");

                    b.Property<string>("FolloweeId");

                    b.HasKey("FollowerId", "FolloweeId");

                    b.HasIndex("FolloweeId");

                    b.HasIndex("FollowerId");

                    b.ToTable("Followings");
                });

            modelBuilder.Entity("GigHub.Models.Genre", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("GigHub.Models.Gig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArtistId")
                        .IsRequired();

                    b.Property<DateTime>("DateTime");

                    b.Property<byte>("GenreId");

                    b.Property<bool>("IsCancelled");

                    b.Property<string>("Venue")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.ToTable("Gigs");
                });

            modelBuilder.Entity("GigHub.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("GigId");

                    b.Property<int>("NotificationType");

                    b.Property<DateTime?>("OriginalDateTime");

                    b.Property<string>("OriginalVenue");

                    b.HasKey("Id");

                    b.HasIndex("GigId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("GigHub.Models.UserNotification", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("NotificationId");

                    b.Property<bool>("IsRead");

                    b.HasKey("UserId", "NotificationId");

                    b.HasIndex("NotificationId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GigHub.Models.Attendance", b =>
                {
                    b.HasOne("GigHub.Models.ApplicationUser", "Attendee")
                        .WithMany()
                        .HasForeignKey("AttendeeId");

                    b.HasOne("GigHub.Models.Gig", "Gig")
                        .WithMany("Attendances")
                        .HasForeignKey("GigId");
                });

            modelBuilder.Entity("GigHub.Models.Following", b =>
                {
                    b.HasOne("GigHub.Models.ApplicationUser", "Followee")
                        .WithMany("Followers")
                        .HasForeignKey("FolloweeId");

                    b.HasOne("GigHub.Models.ApplicationUser", "Follower")
                        .WithMany("Followees")
                        .HasForeignKey("FollowerId");
                });

            modelBuilder.Entity("GigHub.Models.Gig", b =>
                {
                    b.HasOne("GigHub.Models.ApplicationUser", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId");

                    b.HasOne("GigHub.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("GigHub.Models.Notification", b =>
                {
                    b.HasOne("GigHub.Models.Gig", "Gig")
                        .WithMany()
                        .HasForeignKey("GigId");
                });

            modelBuilder.Entity("GigHub.Models.UserNotification", b =>
                {
                    b.HasOne("GigHub.Models.Notification", "Notification")
                        .WithOne()
                        .HasForeignKey("GigHub.Models.UserNotification", "NotificationId");

                    b.HasOne("GigHub.Models.ApplicationUser", "User")
                        .WithMany("UserNotifications")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GigHub.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GigHub.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.HasOne("GigHub.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });
        }
    }
}
