using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class UpdateProviderProfileViewModel
    {
        [Required, Display(Name ="Company Name")]
        public string CompanyName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
    }
}
