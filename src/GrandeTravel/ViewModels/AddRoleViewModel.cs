using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class AddRoleViewModel
    {
        [Display(Name = "New Role"), MaxLength(15)]
        public string  NewRole { get; set; }
    }
}
