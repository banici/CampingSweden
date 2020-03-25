using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampingParkAPI.MappingProfiles;
using CampingParkAPI.Models;
using CampingParkAPI.Models.DTOs;
using CampingParkAPI.Repository;
using CampingParkAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampingParkAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "CampingParkOpenAPI")] // This bundles the Swagger UI Document for this controller only.
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CampingParksV2Controller : ControllerBase
    {
        private readonly ICampingParkRepository _repo;
        private readonly IMapper _mapper;
        public CampingParksV2Controller(ICampingParkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of camping parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CampingParkDTO>))]
        public IActionResult GetCampingParks()
        {
            var objList = _repo.GetAllCampingParks();

            var objDto = _mapper.Map<ICollection<CampingPark>, List<CampingParkDTO>>(objList);

            // This block works just like the oneline mapper converter above.
            //var objDto = new List<CampingParkDTO>();
            //foreach(var obj in objList)
            //{
            //    objDto.Add(_mapper.Map<CampingParkDTO>(obj));
            //}

            return Ok(objDto);
        }
      
    }
}