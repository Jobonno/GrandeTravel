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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers
{
    public class FeedbackController : Controller
    {
        private IRepository<Booking> _bookingRepo;
        private UserManager<MyUser> _userManager;
        private IRepository<TravelPackage> _travelPackageManager;
        private IRepository<Feedback> _feedbackManager;


        public FeedbackController(IRepository<Booking> bookingRepo, UserManager<MyUser> userManager, IRepository<TravelPackage> travelPackageManager, IRepository<Feedback> feedbackManager)
        {
            _feedbackManager = feedbackManager;
            _userManager = userManager;
            _bookingRepo = bookingRepo;
            _travelPackageManager = travelPackageManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult Create(int id)
        {
            TravelPackage tp = _travelPackageManager.GetSingle(t => t.TravelPackageId == id);
            
            CreateFeedbackViewModel vm = new CreateFeedbackViewModel
            {
                TravelPackageId = tp.TravelPackageId,
               
              
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult Create(CreateFeedbackViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                Feedback newfeedback = new Feedback
                {
                    UserName = User.Identity.Name,
                    TravelPackageId = vm.TravelPackageId,
                    MyUserId = userId,
                    Rating = vm.Rating,
                    Comment = vm.Comment
                };
                _feedbackManager.Create(newfeedback);
                return RedirectToAction("Details", "TravelPackage", new { id = newfeedback.TravelPackageId });
            }
            return View(vm);
        }
    }
}
