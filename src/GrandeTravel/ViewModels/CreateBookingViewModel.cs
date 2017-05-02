using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class CreateBookingViewModel
    {
        
        [Display(Name = "Booking Date")]
        [DataType(DataType.Date)]
        
        public DateTime BookingDate { get; set; }

        public int TravelPackageId { get; set; }

        public string MyUserId { get; set; }
        
        public string Name { get; set; }

        [Display(Name = "Number of People")]       
        public int People { get; set; }

     
    }
}
