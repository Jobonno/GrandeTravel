using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class CreateTravelPackageViewModel
    {
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }

        public string Location { get; set; }

        [Display(Name = "Package Description")]
        public string PackageDescription { get; set; }

        [Display(Name = "Package Price")]
        [DataType(DataType.Currency)]
        public int PackagePrice { get; set; }
    }
}
