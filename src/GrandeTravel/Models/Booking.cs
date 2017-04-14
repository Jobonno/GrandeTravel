using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; }

        public int TravelPackageId { get; set; }

        public TravelPackage TravelPackage { get; set; }

        

        
    }
}
