using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravel.ViewModels;
using MimeKit;
using MailKit.Net.Smtp;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(vm.FromAddress, vm.FromAddress));
                message.To.Add(new MailboxAddress("Joe", "jobonno@gmail.com"));
                message.Subject = vm.Subject;
                message.Body = new TextPart("plain")
                {
                    Text = vm.Body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("jobonno@gmail.com", "monteleone82");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return RedirectToAction("index");
            }
            return View(vm);
        }

    }
}
