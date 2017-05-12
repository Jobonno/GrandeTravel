using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.Services
{
    public interface IEmailSender
    {
        void SendEmail(string from, string to, string subject, string message); 
    }
}
