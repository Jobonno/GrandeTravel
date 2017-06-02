using GrandeTravel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.Services
{
    public class GrandeTravelDbContext:IdentityDbContext<MyUser>
    {
        public DbSet<TravelPackage> TblTravelPackage { get; set; }
        public DbSet<Booking> TblBooking { get; set; }
        public DbSet<Feedback> TblFeedback { get; set; }
        public DbSet<TravelProviderProfile> TblTravelProviderProfile { get; set; }
        public DbSet<CustomerProfile> TblCustomerProfile { get; set; }
        public DbSet<Photo> TblPhoto { get; set; }


        //connection string
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=GrandeTravel; Trusted_Connection=True");
        }
    }
}
