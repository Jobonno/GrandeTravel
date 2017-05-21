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
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MailKit.Net.Smtp;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers
{
    public class BookingController : Controller
    {
        private IRepository<Booking> _bookingRepo;
        private UserManager<MyUser> _userManager;
        private IRepository<TravelPackage> _travelPackageManager;
        private IEmailSender _emailService;

        public BookingController(IRepository<Booking> bookingRepo, UserManager<MyUser> userManager, IRepository<TravelPackage> travelPackageManager, IEmailSender emailService)
        {
            _userManager = userManager;
            _bookingRepo = bookingRepo;
            _travelPackageManager = travelPackageManager;
            _emailService = emailService;
        }

        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            IEnumerable<Booking> list = _bookingRepo.Query(b => b.MyUserId == userId);
            DisplayAllBookingsViewModel vm = new DisplayAllBookingsViewModel
            {
                Bookings = list,
                total = list.Count()
            };
            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(int id)
        {
            TravelPackage tp = _travelPackageManager.GetSingle(t => t.TravelPackageId == id);
            string today = DateTime.Now.ToString();
            CreateBookingViewModel vm = new CreateBookingViewModel
            {
                TravelPackageName = tp.PackageName,
                TotalCost = tp.PackagePrice,
                TravelPackageId = id               
                
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookingViewModel vm)
        {
            
            if (ModelState.IsValid)
            {
                TravelPackage tp = _travelPackageManager.GetSingle(t => t.TravelPackageId == vm.TravelPackageId);
                var userId = _userManager.GetUserId(User);
                string voucherCode = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                Booking booking = new Booking
                {
                    BookingDate = vm.BookingDate,
                    TravelPackageId = vm.TravelPackageId,
                    MyUserId = userId,
                    People = vm.People,
                    Name = User.Identity.Name,
                    //xtra Security getting price from database
                    TotalCost = (vm.People * tp.PackagePrice),
                    TravelPackageName = vm.TravelPackageName,
                    VoucherCode = voucherCode,
                    LeftFeedback = false
                };
                _bookingRepo.Create(booking);
                //Send Email
                MyUser user = await _userManager.FindByIdAsync(userId);
                _emailService.SendEmail("grandetravelproject@gmail.com", user.Email, "Your Booking Voucher",
                            "Booking Date : " + booking.BookingDate + "\n" +
                            "Package Name : " + booking.TravelPackageName + "\n" +
                            "Number of People: " + booking.People + "\n" +
                            "Total cost : $" + booking.TotalCost + "\n" +
                            "Expiry Date : " + booking.BookingDate.AddMonths(3) + "\n" +
                            "Voucher Code : " + voucherCode);
                                               

                return RedirectToAction("Details", "TravelPackage", new { id = booking.TravelPackageId});
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public IActionResult PaymentRecieved(int id)
        {
            Booking booking = _bookingRepo.GetSingle(b => b.BookingId == id);
            booking.PaymentReceived = true;
            _bookingRepo.Update(booking);
            return RedirectToAction("Index");
        }
    }
}
