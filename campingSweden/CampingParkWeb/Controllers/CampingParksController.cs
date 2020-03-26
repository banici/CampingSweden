using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampingParkWeb.Models;
using CampingParkWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CampingParkWeb.Controllers
{
    public class CampingParksController : Controller
    {
        private readonly ICampingParkRepository _cpRepo;

        public CampingParksController(ICampingParkRepository cpRepo)
        {
            _cpRepo = cpRepo;
        }
        public IActionResult Index()
        {
            return View(new CampingPark() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            CampingPark obj = new CampingPark();

            if(id == null)
            {
                // this will be true for insert/create
                return View(obj);
            }

            // this will come here for update.
            obj = await _cpRepo.GetAsync(StaticDetails.CampingParkAPIPath, id.GetValueOrDefault());

            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public async Task<IActionResult> GetAllCampingPark()
        {
            return Json(new { data = await _cpRepo.GetAllAsync(StaticDetails.CampingParkAPIPath) }); 
        }
    }
}