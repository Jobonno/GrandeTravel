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
                var list = _travelPackageRepo.GetAll();
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
                var list = _travelPackageRepo.Query(c => c.Location.Contains(location));
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
