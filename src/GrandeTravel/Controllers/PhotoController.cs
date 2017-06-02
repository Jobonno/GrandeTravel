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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers
{

    public class PhotoController : Controller
    {
        private UserManager<MyUser> _userManager;
        private IRepository<TravelPackage> _TravelPackageRepo;
     
        private IHostingEnvironment _HostingEnviro;
        
        private IRepository<Photo> _photoRepo;

        public PhotoController(IRepository<TravelPackage> TravelPackagerepo, IHostingEnvironment HostingEnviro, UserManager<MyUser> userManager, IRepository<Photo> photoRepo)
        {
            _photoRepo = photoRepo;
            _TravelPackageRepo = TravelPackagerepo;           
            _HostingEnviro = HostingEnviro;
            _userManager = userManager;         
           
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Create(int id)
        {
            CreatePhotoViewModel vm = new CreatePhotoViewModel
            {
                TravelPackageId = id
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePhotoViewModel vm, IList<IFormFile> PhotoLocation)
        {
            if (ModelState.IsValid)
            {
                if(PhotoLocation != null)
                {
                    int count = 2;
                    TravelPackage tp = _TravelPackageRepo.GetSingle(t => t.TravelPackageId == vm.TravelPackageId);
                    foreach (var item in PhotoLocation)
                    {
                        string uploadPath = Path.Combine(_HostingEnviro.WebRootPath, "Media\\TravelPackage");
                        string filename = User.Identity.Name + "-" + tp.PackageName +"-" + count + Path.GetExtension(item.FileName);
                        uploadPath = Path.Combine(uploadPath, filename);
                        using (FileStream fs = new FileStream(uploadPath, FileMode.Create))
                        {
                            item.CopyTo(fs);
                        }
                        string SaveFilename = Path.Combine("Media\\TravelPackage", filename);
                        Photo tempphoto = new Photo
                        {
                            PhotoLocation = SaveFilename,
                            TravelPackageId = vm.TravelPackageId
                        };
                        _photoRepo.Create(tempphoto);
                        count++;
                    }
                    return RedirectToAction("Details", "TravelPackage", new { id = vm.TravelPackageId });
                }
                
            }
            return View(vm);
        }


    }
}
