using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CampingParkWeb.Models;
using CampingParkWeb.Repository.IRepository;
using CampingParkWeb.Models.ViewModel;

namespace CampingParkWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICampingParkRepository _cpReo;
        private readonly ITrailRepository _tRepo;

        public HomeController(ILogger<HomeController> logger, ICampingParkRepository cpRepo, ITrailRepository tRepo)
        {
            _logger = logger;
            _cpReo = cpRepo;
            _tRepo = tRepo;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM listOfCampingParksAndTrails = new IndexVM()
            {
                CampingParkList = await _cpReo.GetAllAsync(StaticDetails.CampingParkAPIPath),
                TrailList = await _tRepo.GetAllAsync(StaticDetails.TrailAPIPath)
            };
            return View(listOfCampingParksAndTrails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
