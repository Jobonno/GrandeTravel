using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravel.Services;
using GrandeTravel.Models;
using GrandeTravel.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Xml.Linq;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers
{
    public class TravelPackageController : Controller
    {
        private UserManager<MyUser> _userManager;
        private IRepository<TravelPackage> _TravelPackageRepo;
        private IRepository<Booking> _BookingRepo;
        private IHostingEnvironment _HostingEnviro;
        private IRepository<Feedback> _feedbackRepo;
        private IRepository<TravelProviderProfile> _travelProfileRepo;

        public TravelPackageController(IRepository<TravelPackage> TravelPackagerepo, IRepository<Booking> bookingRepo, IHostingEnvironment HostingEnviro, UserManager<MyUser> userManager, IRepository<Feedback> feedbackRepo, IRepository<TravelProviderProfile> travelProfileRepo)
        {
            _TravelPackageRepo = TravelPackagerepo;
            _BookingRepo = bookingRepo;
            _HostingEnviro = HostingEnviro;
            _userManager = userManager;
            _feedbackRepo = feedbackRepo;
            _travelProfileRepo = travelProfileRepo;
        }


        //GET: /<controller>/
        public IActionResult Index(string searchString, int minPrice, int maxPrice)
        {
            IEnumerable<TravelPackage> list;


            if (!String.IsNullOrEmpty(searchString))
            {
                list = _TravelPackageRepo.Query(b => b.Location.Contains(searchString));
                if (maxPrice > 0 || minPrice > 0)
                {
                    if (minPrice > maxPrice)
                    {
                        list = list.Where(b => b.PackagePrice >= minPrice);
                    }
                    else
                    {
                        list = list.Where(b => (b.PackagePrice <= maxPrice && b.PackagePrice >= minPrice));
                    }
                }

            }
            else if (maxPrice > 0 || minPrice > 0)
            {
                //only if minprice is entered
                if (minPrice > maxPrice)
                {
                    list = _TravelPackageRepo.Query(b => b.PackagePrice >= minPrice);
                }
                else
                {
                    list = _TravelPackageRepo.Query(b => (b.PackagePrice <= maxPrice && b.PackagePrice >= minPrice));
                }
            }
            else
            {
                list = _TravelPackageRepo.GetAll();
            }
            //Display Only TravelProviders Packages
            if (User.IsInRole("TravelProvider"))
            {
                list = list.Where(id => id.MyUserId == _userManager.GetUserId(User));
            }
            DisplayAllTravelPackagesViewModel vm = new DisplayAllTravelPackagesViewModel
            {
                Total = list.Count(),
                TravelPackageList = list
            };
            return View(vm);
        }




        [HttpGet]
        [Authorize(Roles = "TravelProvider,Admin")]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [Authorize(Roles = "TravelProvider,Admin")]
        public IActionResult Create(CreateTravelPackageViewModel vm, IFormFile PhotoLocation)
        {
            if (ModelState.IsValid)
            {
                var id = _userManager.GetUserId(User);
                TravelProviderProfile tpp = _travelProfileRepo.GetSingle(t => t.UserId == id);
                string providerName;
                if (tpp == null)
                {
                    providerName = "";
                }
                else
                {
                    providerName = tpp.CompanyName;
                }
                //map the tp props with the viewmodel
                TravelPackage tp = new TravelPackage
                {
                    PackageName = vm.PackageName,
                    Location = vm.Location,
                    PackageDescription = vm.PackageDescription,
                    PackagePrice = vm.PackagePrice,
                    ProviderName = providerName,
                    MyUserId = id

                };
                if (PhotoLocation != null)
                {
                    string uploadPath = Path.Combine(_HostingEnviro.WebRootPath, "Media\\TravelPackage");
                    uploadPath = Path.Combine(uploadPath, User.Identity.Name);
                    Directory.CreateDirectory(Path.Combine(uploadPath, tp.PackageName));
                    string filename = Path.GetFileName(PhotoLocation.FileName);

                    using (FileStream fs = new FileStream(Path.Combine(uploadPath, tp.PackageName, filename), FileMode.Create))
                    {
                        PhotoLocation.CopyTo(fs);
                    }
                    string SaveFilename = Path.Combine(User.Identity.Name, tp.PackageName, filename);                  

                    
                    tp.PhotoLocation = SaveFilename;
                }

                //call the service to add the package
                _TravelPackageRepo.Create(tp);
                return RedirectToAction("Index", "TravelPackage");
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            TravelPackage tp = _TravelPackageRepo.GetSingle(t => t.TravelPackageId == id);
            IEnumerable<Booking> list = _BookingRepo.Query(b => b.TravelPackageId == id);
            IEnumerable<Feedback> feedbacks = _feedbackRepo.Query(f => f.TravelPackageId == id);
            MyUser travelProviderName = await _userManager.FindByIdAsync(tp.MyUserId);
            string TpName = travelProviderName.UserName;
            //Google Maps 
            var address = tp.Location;
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

            var request = WebRequest.Create(requestUri);
            var response =await  request.GetResponseAsync();
            var xdoc = XDocument.Load(response.GetResponseStream());
            var result = xdoc.Element("GeocodeResponse").Element("result");
            var locationElement = result.Element("geometry").Element("location");
            var lat = locationElement.Element("lat").Value;
            var lng = locationElement.Element("lng").Value;

            DisplaySingleTravelPackageViewModel vm = new DisplaySingleTravelPackageViewModel
            {
                PackageName = tp.PackageName,
                TravelPackageId = tp.TravelPackageId,
                Location = tp.Location,
                PhotoLocation = tp.PhotoLocation,
                PackageDescription = tp.PackageDescription,
                PackagePrice = tp.PackagePrice,
                Bookings = list,
                Feedbacks = feedbacks,
                TravelProviderName = tp.ProviderName,
                UserName = TpName,
                latitude = lat,
                longitude = lng


            };

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "TravelProvider,Admin")]
        public IActionResult Update(int id)
        {
            TravelPackage tp = _TravelPackageRepo.GetSingle(t => t.TravelPackageId == id);
            if (tp != null)
            {
                UpdateTravelPackageViewModel vm = new UpdateTravelPackageViewModel
                {
                    PackageName = tp.PackageName,
                    Location = tp.Location,
                    PhotoLocation = tp.PhotoLocation,
                    PackageDescription = tp.PackageDescription,
                    PackagePrice = tp.PackagePrice
                };
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "TravelProvider,Admin")]
        public IActionResult Update(int id, UpdateTravelPackageViewModel vm, IFormFile PhotoLocation)
        {
            TravelPackage tp = _TravelPackageRepo.GetSingle(t => t.TravelPackageId == id);
            if (ModelState.IsValid && tp != null)
            {
                tp.PackageName = vm.PackageName;
                tp.Location = vm.Location;
                tp.PackageDescription = vm.PackageDescription;
                tp.PackagePrice = vm.PackagePrice;
                if (PhotoLocation != null)
                {
                    string uploadPath = Path.Combine(_HostingEnviro.WebRootPath, "Media\\TravelPackage");
                    uploadPath = Path.Combine(uploadPath, User.Identity.Name);
                    uploadPath = Path.Combine(uploadPath, tp.PackageName);
                    string filename = Path.GetFileName(PhotoLocation.FileName);

                    using (FileStream fs = new FileStream(Path.Combine(uploadPath, filename), FileMode.Create))
                    {
                        PhotoLocation.CopyTo(fs);
                    }
                    string SaveFilename = Path.Combine(User.Identity.Name, tp.PackageName, filename);
                    tp.PhotoLocation = SaveFilename;
                }

                _TravelPackageRepo.Update(tp);
                return RedirectToAction("Details", new { id = tp.TravelPackageId });
            }

            //if Invalid
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "TravelProvider,Admin")]
        public IActionResult Delete(int id)
        {
            TravelPackage tp = _TravelPackageRepo.GetSingle(t => t.TravelPackageId == id);
            //check if It is Their Own Travel Package
            if (tp != null && (tp.MyUserId == _userManager.GetUserId(User) || User.IsInRole("Admin")))
            {
                _TravelPackageRepo.Delete(tp);

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "TravelProvider,Admin")]
        public IActionResult Statistics()
        {
            IEnumerable<TravelPackage> tpList = _TravelPackageRepo.Query(i => i.MyUserId == _userManager.GetUserId(User)).ToList();
            List<string> names = new List<string>();
            List<string> NoBookings = new List<string>();
            List<string> values = new List<string>();
            foreach (var item in tpList)
            {
                names.Add("\"" + item.PackageName +"\"");
                values.Add(_BookingRepo.Query(t => t.TravelPackageName == item.PackageName).Sum(r => r.TotalCost).ToString());
                NoBookings.Add(_BookingRepo.Query(b => b.TravelPackageName == item.PackageName).Count().ToString());

            }

            string PackageNames = string.Join(",", names);
            string SalesTotal = string.Join(",", values);
            string BookingsTotal = string.Join(",", NoBookings);

            StatisticsViewModel vm = new StatisticsViewModel
            {
                Labels = PackageNames,
                Data = SalesTotal,
                AmountBookings = BookingsTotal
            };
            
            return View(vm);
        }
    }
}
