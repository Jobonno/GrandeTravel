﻿using GrandeTravel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.Services
{
    public class GrandeTravelDbContext:DbContext
    {
        public DbSet<TravelPackage> TblTravelPackage { get; set; }
        public DbSet<Booking> TblBooking { get; set; }


        //connection string
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=GrandeTravel; Trusted_Connection=True");
        }
    }
}