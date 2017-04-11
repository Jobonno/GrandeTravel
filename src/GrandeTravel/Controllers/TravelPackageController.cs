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
        private IRepository<Booking> _BookingRepo;


        public TravelPackageController(IRepository<TravelPackage> TravelPackagerepo, IRepository<Booking> bookingRepo)
        {
            _TravelPackageRepo = TravelPackagerepo;
            _BookingRepo = bookingRepo;
        }


        //GET: /<controller>/
        public IActionResult Index(string searchString)
        {
            IEnumerable<TravelPackage> list;

            if (!String.IsNullOrEmpty(searchString))
            {
                list = _TravelPackageRepo.Query(b => b.Location.Contains(searchString));
            }else
            {
                list = _TravelPackageRepo.GetAll();
            }
            
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

        [HttpGet]
        public IActionResult Details(int id)
        {
            TravelPackage tp = _TravelPackageRepo.GetSingle(t => t.TravelPackageId == id);
            IEnumerable<Booking> list = _BookingRepo.Query(b => b.TravelPackageId == id);
            DisplaySingleTravelPackageViewModel vm = new DisplaySingleTravelPackageViewModel
            {
                PackageName = tp.PackageName,
                TravelPackageId = tp.TravelPackageId,
                Location = tp.Location,
                PackageDescription = tp.PackageDescription,
                PackagePrice = tp.PackagePrice,
                Bookings = list

        };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            TravelPackage tp = _TravelPackageRepo.GetSingle(t => t.TravelPackageId == id);
            if (tp != null)
            {
                UpdateTravelPackageViewModel vm = new UpdateTravelPackageViewModel
                {
                    PackageName = tp.PackageName,
                    Location = tp.Location,
                    PackageDescription = tp.PackageDescription,
                    PackagePrice = tp.PackagePrice
                };
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id, UpdateTravelPackageViewModel vm)
        {
            TravelPackage tp = _TravelPackageRepo.GetSingle(t => t.TravelPackageId == id);
            if (ModelState.IsValid && tp != null)
            {
                tp.PackageName = vm.PackageName;
                tp.Location = vm.Location;
                tp.PackageDescription = vm.PackageDescription;
                tp.PackagePrice = vm.PackagePrice;

                _TravelPackageRepo.Update(tp);
                return RedirectToAction("Details", new { id = tp.TravelPackageId });
            }

            //if Invalid
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TravelPackage tp = _TravelPackageRepo.GetSingle(t => t.TravelPackageId == id);
            if (tp != null)
            {
                _TravelPackageRepo.Delete(tp);
                
            }
            return RedirectToAction("Index");
        }
    }
}
