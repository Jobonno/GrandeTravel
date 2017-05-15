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

        public BookingController(IRepository<Booking> bookingRepo, UserManager<MyUser> userManager, IRepository<TravelPackage> travelPackageManager)
        {
            _userManager = userManager;
            _bookingRepo = bookingRepo;
            _travelPackageManager = travelPackageManager;
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
                var userId = _userManager.GetUserId(User);
                Booking booking = new Booking
                {
                    BookingDate = vm.BookingDate,
                    TravelPackageId = vm.TravelPackageId,
                    MyUserId = userId,
                    People = vm.People,
                    Name = User.Identity.Name,
                    TotalCost = (vm.People * vm.TotalCost),
                    TravelPackageName = vm.TravelPackageName
                };
                _bookingRepo.Create(booking);
                //Send Email
                var message = new MimeMessage();
                MyUser user = await _userManager.FindByIdAsync(userId);
                message.From.Add(new MailboxAddress("Grande Travel", "grandetravelproject@gmail.com"));
                message.To.Add(new MailboxAddress(user.UserName, user.Email));
                message.Subject = "Your Booking Voucher";
                message.Body = new TextPart("plain")
                {
                    Text = "Booking Date : " + booking.BookingDate + "\n" +
                           "Package Name : " + booking.TravelPackageName + "\n" +
                           "Number of People: " + booking.People + "\n" +
                           "Total cost : $" + booking.TotalCost + "\n" +
                           "Expiry Date : " + booking.BookingDate.AddMonths(3)
                           //add voucher code here

                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("grandetravelproject@gmail.com", "Diplomaproject");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return RedirectToAction("Details", "TravelPackage", new { id = booking.TravelPackageId});
            }
            return View(vm);
        }
    }
}
