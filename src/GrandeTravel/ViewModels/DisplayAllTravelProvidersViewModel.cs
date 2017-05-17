using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeTravel.Models;

namespace GrandeTravel.ViewModels
{
    public class DisplayAllTravelProvidersViewModel
    {
        public IEnumerable<MyUser> TravelProviders { get; set; }

    }
}
