﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.ViewModels
{
    public class CreatePhotoViewModel
    {
        public int TravelPackageId { get; set; }
        public string  PhotoLocation { get; set; }
    }
}