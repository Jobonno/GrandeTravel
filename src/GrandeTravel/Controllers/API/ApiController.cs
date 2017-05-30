using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravel.Services;
using GrandeTravel.Models;
using GrandeTravel.ViewModels;
using System.Net;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravel.Controllers.API
{
   
    public class ApiController : Controller
    {
        private IRepository<TravelPackage> _travelPackageRepo;

        public ApiController(IRepository<TravelPackage> travelPackageRepo)
        {
            _travelPackageRepo = travelPackageRepo;
        }

       [HttpGet("api/getAll")]
        public JsonResult GetAll()
        {
            try
            {
                var list = _travelPackageRepo.Query(p => !p.Discontinued);
                return Json(list);
            }
            catch (Exception ex)
            {

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }
            
        }

        [HttpPost("api/PackageByDesc")]
        public JsonResult SearchPackageDesc(string description)
        {
            try
            {
                var list = _travelPackageRepo.Query(c => c.PackageDescription.Contains(description) && !c.Discontinued);
                return Json(list);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

        }

        [HttpPost("api/PackageByLocation")]
        public JsonResult SearchPackage(string location)
        {
            try
            {
                var list = _travelPackageRepo.Query(c => c.Location.Contains(location) && !c.Discontinued);
                return Json(list);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

        }

        [HttpGet("api/getLocations")]
        public JsonResult GetLocations()
        {
            try
            {
                var list = _travelPackageRepo.Query(p => !p.Discontinued).Select(p => p.Location).Distinct();
               
                return Json(list);
            }
            catch (Exception ex)
            {

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

        }
    }
}
