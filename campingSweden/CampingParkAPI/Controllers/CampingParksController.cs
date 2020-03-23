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
    public class CampingParksController : ControllerBase
    {
        private readonly ICampingParkRepository _repo;
        private readonly IMapper _mapper;
        public CampingParksController(ICampingParkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCampingParks()
        {
            var objList = _repo.GetAllCampingParks();

            var objDto = _mapper.Map<ICollection<CampingPark>, List<CampingParkDTO>>(objList);
            //var objDto = new List<CampingParkDTO>();
            //foreach(var obj in objList)
            //{
            //    objDto.Add(_mapper.Map<CampingParkDTO>(obj));
            //}

            return Ok(objDto);
        }

        [HttpGet ("{id:int}", Name = "GetCampingPark")]
        public IActionResult GetCampingPark(int id)
        {
            var obj = _repo.GetCampingPark(id);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<CampingParkDTO>(obj);

            return Ok(objDto);
        }
    }
}