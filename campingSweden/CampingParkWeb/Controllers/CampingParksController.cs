﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CampingParkWeb.Models;
using CampingParkWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampingParkWeb.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            CampingPark obj = new CampingPark();

            if(id == null)
            {
                // this will be true for insert/create
                return View(obj);
            }

            // this will come here for update.
            obj = await _cpRepo.GetAsync(StaticDetails.CampingParkAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CampingPark obj)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count > 0)
                {
                    byte[] p1 = null;
                    using(var _fileStream = files[0].OpenReadStream())
                    {
                        using(var _memoryStream = new MemoryStream())
                        {
                            _fileStream.CopyTo(_memoryStream);
                            p1 = _memoryStream.ToArray();
                        }
                    }

                    obj.Picture = p1;
                }
                else
                {
                    var objFromDb = await _cpRepo.GetAsync(StaticDetails.CampingParkAPIPath, obj.Id, HttpContext.Session.GetString("JWToken"));
                    obj.Picture = objFromDb.Picture;
                }
                if(obj.Id == 0)
                {
                    await _cpRepo.CreateAsync(StaticDetails.CampingParkAPIPath, obj, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _cpRepo.UpdateAsync(StaticDetails.CampingParkAPIPath + obj.Id, obj, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllCampingPark()
        {
            return Json(new { data = await _cpRepo.GetAllAsync(StaticDetails.CampingParkAPIPath, HttpContext.Session.GetString("JWToken")) }); 
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _cpRepo.DeleteAsync(StaticDetails.CampingParkAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if(status)
            {
                return Json(new { success = true, message = "Delete Successful!" });
            }
            return Json(new { success = false, message = "Delete Not Successful!" });
        }
    }
}