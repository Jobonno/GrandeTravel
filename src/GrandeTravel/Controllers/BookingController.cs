using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravel.Services;
using GrandeTravel.Models;
using GrandeTravel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers
{
    public class BookingController : Controller
    {
        private IRepository<Booking> _bookingRepo;
        private UserManager<MyUser> _userManager;

        public BookingController(IRepository<Booking> bookingRepo, UserManager<MyUser> userManager)
        {
            _userManager = userManager;
            _bookingRepo = bookingRepo;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var userId = _userManager.GetUserId(User);
            CreateBookingViewModel vm = new CreateBookingViewModel
            {
                TravelPackageId = id,
                MyUserId = userId
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(CreateBookingViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Booking booking = new Booking
                {
                    BookingDate = vm.BookingDate,
                    TravelPackageId = vm.TravelPackageId,
                    MyUserId = vm.MyUserId
                };
                _bookingRepo.Create(booking);
                return RedirectToAction("Index", "TravelPackage");
            }
            return View(vm);
        }
    }
}
