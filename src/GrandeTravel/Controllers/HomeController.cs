using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravel.ViewModels;
using MimeKit;
using MailKit.Net.Smtp;
using GrandeTravel.Services;
using GrandeTravel.Models;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers
{
    public class HomeController : Controller
    {
        private IEmailSender _emailService;
        private IRepository<TravelPackage> _TravelPackageRepo;

        public HomeController(IEmailSender emailService, IRepository<TravelPackage> TravelPackageRepo)
        {
            _TravelPackageRepo = TravelPackageRepo;
            _emailService = emailService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<string> names = new List<string>();
            var list = _TravelPackageRepo.Query(p =>!p.Discontinued);
            foreach (var item in list)
            {
                names.Add(item.PackageName);                
            }
            var json = JsonConvert.SerializeObject(names);
            SearchIndexViewModel vm = new SearchIndexViewModel
            {
                list = json
            };
            return View(vm);
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
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
             
                _emailService.SendEmail(vm.FromAddress, "grandetravelproject@gmail.com", vm.Subject, vm.Body);

                return RedirectToAction("index");
            }
            return View(vm);
        }

    }
}
