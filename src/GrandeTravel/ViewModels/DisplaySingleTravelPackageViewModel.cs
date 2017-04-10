using GrandeTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class DisplaySingleTravelPackageViewModel
    {
        public int TravelPackageId { get; set; }


        public string PackageName { get; set; }

        public string Location { get; set; }


        public string PackageDescription { get; set; }


        public int PackagePrice { get; set; }

        //One TravelPackage has many Bookings
        public IEnumerable<Booking> Bookings { get; set; }
    }
}
