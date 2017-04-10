using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravel.Services;
using GrandeTravel.Models;
using GrandeTravel.ViewModels;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers
{
    public class TravelPackageController : Controller
    {
        private IRepository<TravelPackage> _TravelPackageRepo;

        public TravelPackageController(IRepository<TravelPackage> repo)
        {
            _TravelPackageRepo = repo;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<TravelPackage> list = _TravelPackageRepo.GetAll();
            DisplayAllTravelPackagesViewModel vm = new DisplayAllTravelPackagesViewModel
            {
                Total = list.Count(),
                TravelPackageList = list
            };
            return View(vm);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateTravelPackageViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //map the tp props with the viewmodel
                TravelPackage tp = new TravelPackage
                {
                    PackageName = vm.PackageName,
                    Location = vm.Location,
                    PackageDescription = vm.PackageDescription,
                    PackagePrice = vm.PackagePrice
                };
                //call the service to add the package
                _TravelPackageRepo.Create(tp);
                return RedirectToAction("Index", "TravelPackage");
            }

            return View(vm);
        }
    }
}
