using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class CreateTravelPackageViewModel
    {

        [Required, Display(Name = "Package Name")]
        public string PackageName { get; set; }
        [Required]
        public string Location { get; set; }

        [Display(Name = "Main Photo")]
        public string PhotoLocation { get; set; }
        

        [Required, Display(Name = "Package Description")]
        public string PackageDescription { get; set; }

        [Required, Display(Name = "Package Price")]
        [DataType(DataType.Currency)]
        public int PackagePrice { get; set; }

       

    }
}
