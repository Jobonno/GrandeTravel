using GrandeTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class DisplayAllTravelPackagesViewModel
    {
        public int Total { get; set; }

        public IEnumerable<TravelPackage> TravelPackageList { get; set; }

        public string searchList;

    }
}
