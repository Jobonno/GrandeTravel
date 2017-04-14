using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GrandeTravel.Services;

namespace GrandeTravel.Migrations
{
    [DbContext(typeof(GrandeTravelDbContext))]
    [Migration("20170414130425_AddFeedbackModel")]
    partial class AddFeedbackModel
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

                    b.Property<int>("TravelPackageId");

                    b.HasKey("BookingId");

                    b.HasIndex("TravelPackageId");

                    b.ToTable("TblBooking");
                });

            modelBuilder.Entity("GrandeTravel.Models.TravelPackage", b =>
                {
                    b.Property<int>("TravelPackageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Location");

                    b.Property<string>("PackageDescription");

                    b.Property<string>("PackageName");

                    b.Property<int>("PackagePrice");

                    b.Property<string>("PhotoLocation");

                    b.HasKey("TravelPackageId");

                    b.ToTable("TblTravelPackage");
                });

            modelBuilder.Entity("GrandeTravel.Models.Booking", b =>
                {
                    b.HasOne("GrandeTravel.Models.TravelPackage", "TravelPackage")
                        .WithMany("Bookings")
                        .HasForeignKey("TravelPackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
