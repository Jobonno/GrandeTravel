using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.Models
{
    public class MyUser:IdentityUser
    {
        IEnumerable<TravelPackage> TravelPackages { get; set; }
        IEnumerable<Booking> Bookings { get; set; }
        IEnumerable<Feedback> Feedbacks { get; set; }

    }
}
