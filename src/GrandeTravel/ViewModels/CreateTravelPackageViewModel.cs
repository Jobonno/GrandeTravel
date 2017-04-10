using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class CreateTravelPackageViewModel
    {
        public string PackageName { get; set; }

        public string Location { get; set; }

        public string PackageDescription { get; set; }

        public int PackagePrice { get; set; }
    }
}
