using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampingParkAPI.MappingProfiles;
using CampingParkAPI.Models;
using CampingParkAPI.Models.DTOs;
using CampingParkAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampingParkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampingParksController : ControllerBase
    {
        private readonly CampingParkRepository _repo;
        private readonly IMapper _mapper;
        public CampingParksController(CampingParkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCampingParks()
        {
            var objList = _repo.GetAllCampingParks();

            //var dtoList = _mapper.Map<ICollection<CampingPark>, ICollection<CampingParkDTO>>(objList);
            var objDto = new List<CampingParkDTO>();
            foreach(var obj in objList)
            {
                objDto.Add(_mapper.Map<CampingParkDTO>(obj));
            }

            return Ok(objDto);
        }
    }
}