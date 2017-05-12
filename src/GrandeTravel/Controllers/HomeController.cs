using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravel.ViewModels;
using MimeKit;
using MailKit.Net.Smtp;
using GrandeTravel.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers
{
    public class HomeController : Controller
    {
        private IEmailSender _emailService;

        public HomeController(IEmailSender emailService)
        {
            _emailService = emailService;
        }
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
                //var message = new MimeMessage();
                //message.From.Add(new MailboxAddress(vm.FromAddress, vm.FromAddress));
                //message.To.Add(new MailboxAddress("Grande Travel", "grandetravelproject@gmail.com"));
                //message.Subject = vm.Subject;
                //message.Body = new TextPart("plain")
                //{
                //    Text = vm.Body
                //};

                //using (var client = new SmtpClient())
                //{
                //    client.Connect("smtp.gmail.com", 587, false);
                //    client.AuthenticationMechanisms.Remove("XOAUTH2");
                //    client.Authenticate("grandetravelproject@gmail.com", "Diplomaproject");
                //    client.Send(message);
                //    client.Disconnect(true);
                //}
                _emailService.SendEmail(vm.FromAddress, "grandetravelproject@gmail.com", vm.Subject, vm.Body);

                return RedirectToAction("index");
            }
            return View(vm);
        }

    }
}
