using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.Models
{
    public class TravelPackage
    {
        public int TravelPackageId { get; set; }

       
        public string PackageName { get; set; }

        public string Location { get; set; }

        
        public string PackageDescription { get; set; }

      
        public int PackagePrice { get; set; }
    }
}
