using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GrandeTravel.Services;

namespace GrandeTravel.Migrations
{
    [DbContext(typeof(GrandeTravelDbContext))]
    [Migration("20170410114707_AddTravelPackageDetailView")]
    partial class AddTravelPackageDetailView
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GrandeTravel.Models.TravelPackage", b =>
                {
                    b.Property<int>("TravelPackageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Location");

                    b.Property<string>("PackageDescription");

                    b.Property<string>("PackageName");

                    b.Property<int>("PackagePrice");

                    b.HasKey("TravelPackageId");

                    b.ToTable("TblTravelPackage");
                });
        }
    }
}
