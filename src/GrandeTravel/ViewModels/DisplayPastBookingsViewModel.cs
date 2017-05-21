using GrandeTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class DisplayPastBookingsViewModel
    {

        public int total { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
    }
}
