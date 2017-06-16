﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GrandeTravel.Services;

namespace GrandeTravel.Migrations
{
    [DbContext(typeof(GrandeTravelDbContext))]
    [Migration("20170602002059_AddPhotosTable")]
    partial class AddPhotosTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GrandeTravel.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BookingDate");

                    b.Property<bool>("LeftFeedback");

                    b.Property<string>("MyUserId");

                    b.Property<string>("Name");

                    b.Property<bool>("PaymentReceived");

                    b.Property<int>("People");

                    b.Property<int>("TotalCost");

                    b.Property<int>("TravelPackageId");

                    b.Property<string>("TravelPackageName");

                    b.Property<string>("VoucherCode");

                    b.HasKey("BookingId");

                    b.HasIndex("MyUserId");

                    b.HasIndex("TravelPackageId");

                    b.ToTable("TblBooking");
                });

            modelBuilder.Entity("GrandeTravel.Models.CustomerProfile", b =>
                {
                    b.Property<int>("CustomerProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("Phone");

                    b.Property<string>("UserId");

                    b.HasKey("CustomerProfileId");

                    b.ToTable("TblCustomerProfile");
                });

            modelBuilder.Entity("GrandeTravel.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<string>("MyUserId");

                    b.Property<byte>("Rating");

                    b.Property<int>("TravelPackageId");

                    b.Property<string>("UserName");

                    b.HasKey("FeedbackId");

                    b.HasIndex("MyUserId");

                    b.HasIndex("TravelPackageId");

                    b.ToTable("TblFeedback");
                });

            modelBuilder.Entity("GrandeTravel.Models.MyUser", b =>
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

            modelBuilder.Entity("GrandeTravel.Models.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PhotoLocation");

                    b.Property<int>("TravelPackageId");

                    b.HasKey("PhotoId");

                    b.HasIndex("TravelPackageId");

                    b.ToTable("TblPhoto");
                });

            modelBuilder.Entity("GrandeTravel.Models.TravelPackage", b =>
                {
                    b.Property<int>("TravelPackageId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Discontinued");

                    b.Property<string>("Location");

                    b.Property<string>("MyUserId");

                    b.Property<string>("PackageDescription");

                    b.Property<string>("PackageName");

                    b.Property<int>("PackagePrice");

                    b.Property<string>("PhotoLocation");

                    b.Property<string>("ProviderName");

                    b.HasKey("TravelPackageId");

                    b.HasIndex("MyUserId");

                    b.ToTable("TblTravelPackage");
                });

            modelBuilder.Entity("GrandeTravel.Models.TravelProviderProfile", b =>
                {
                    b.Property<int>("TravelProviderProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<int>("Phone");

                    b.Property<string>("UserId");

                    b.HasKey("TravelProviderProfileId");

                    b.ToTable("TblTravelProviderProfile");
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

            modelBuilder.Entity("GrandeTravel.Models.Booking", b =>
                {
                    b.HasOne("GrandeTravel.Models.MyUser", "MyUser")
                        .WithMany()
                        .HasForeignKey("MyUserId");

                    b.HasOne("GrandeTravel.Models.TravelPackage", "TravelPackage")
                        .WithMany("Bookings")
                        .HasForeignKey("TravelPackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GrandeTravel.Models.Feedback", b =>
                {
                    b.HasOne("GrandeTravel.Models.MyUser", "MyUser")
                        .WithMany()
                        .HasForeignKey("MyUserId");

                    b.HasOne("GrandeTravel.Models.TravelPackage", "TravelPackage")
                        .WithMany("Feedbacks")
                        .HasForeignKey("TravelPackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GrandeTravel.Models.Photo", b =>
                {
                    b.HasOne("GrandeTravel.Models.TravelPackage", "TravelPackage")
                        .WithMany("Photos")
                        .HasForeignKey("TravelPackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GrandeTravel.Models.TravelPackage", b =>
                {
                    b.HasOne("GrandeTravel.Models.MyUser", "MyUser")
                        .WithMany()
                        .HasForeignKey("MyUserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GrandeTravel.Models.MyUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GrandeTravel.Models.MyUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GrandeTravel.Models.MyUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}