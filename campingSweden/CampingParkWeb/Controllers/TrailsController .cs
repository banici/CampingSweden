using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CampingParkWeb.Models;
using CampingParkWeb.Repository.IRepository;
using CampingParkWeb;
using CampingParkWeb.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace TrailWeb.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class TrailsController : Controller
    {
        private readonly ITrailRepository _tRepo;
        private readonly ICampingParkRepository _cpRepo;

        public TrailsController(ITrailRepository tRepo, ICampingParkRepository cpRepo)
        {
            _tRepo = tRepo;
            _cpRepo = cpRepo;
        }
        public IActionResult Index()
        {
            return View(new Trail() { });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<CampingPark> cpList = await _cpRepo.GetAllAsync(StaticDetails.CampingParkAPIPath, HttpContext.Session.GetString("JWToken"));

            TrailsVM objVM = new TrailsVM()
            {
                CampingParkList = cpList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Trail = new Trail()
            };


            if(id == null)
            {
                // this will be true for insert/create
                return View(objVM);
            }

            // this will come here for update.
            objVM.Trail = await _tRepo.GetAsync(StaticDetails.TrailAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if(objVM.Trail == null)
            {
                return NotFound();
            }

            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailsVM obj)
        {
            if(ModelState.IsValid)
            {              
                if(obj.Trail.Id == 0)
                {
                    await _tRepo.CreateAsync(StaticDetails.TrailAPIPath, obj.Trail, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _tRepo.UpdateAsync(StaticDetails.TrailAPIPath + obj.Trail.Id, obj.Trail, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<CampingPark> cpList = await _cpRepo.GetAllAsync(StaticDetails.CampingParkAPIPath, HttpContext.Session.GetString("JWToken"));

                TrailsVM objVM = new TrailsVM()
                {
                    CampingParkList = cpList.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    Trail = obj.Trail
                };

                return View(objVM);
            }
        }

        public async Task<IActionResult> GetAllTrail()
        {
            return Json(new { data = await _tRepo.GetAllAsync(StaticDetails.TrailAPIPath, HttpContext.Session.GetString("JWToken")) }); 
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _tRepo.DeleteAsync(StaticDetails.TrailAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if(status)
            {
                return Json(new { success = true, message = "Delete Successful!" });
            }
            return Json(new { success = false, message = "Delete Not Successful!" });
        }
    }
}