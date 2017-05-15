using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class UpdateCustomerProfileViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

      
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
    }
}
