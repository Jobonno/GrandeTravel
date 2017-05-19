using GrandeTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class DisplayAllCustomersViewModel
    {
        public IEnumerable<MyUser> Customers { get; set; }
    }
}
