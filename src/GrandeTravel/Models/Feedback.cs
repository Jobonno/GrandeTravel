using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        public string Comment { get; set; }

        public RatingEnum Rating { get; set; }

        public int TravelPackageId { get; set; }

        public TravelPackage TravelPackage { get; set; }

        

    }

    public enum RatingEnum
    {
         ONESTAR = 1, TWOSTARS = 2, THREESTARS = 3, FOURSTARS = 4, FIVESTARS = 5
    }
}
