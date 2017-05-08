using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class ContactViewModel
    {
        [Required, DataType(DataType.EmailAddress), Display(Name ="Email Address")]
        public string FromAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
