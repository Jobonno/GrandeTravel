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

        public string MyUserId { get; set; }
        public MyUser MyUser { get; set; }

        public string Name { get; set; }

        public int People { get; set; }

        public int TotalCost { get; set; }

        public string TravelPackageName { get; set; }

        public string VoucherCode { get; set; }

    }
}
