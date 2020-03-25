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
    public class CampingParksController : ControllerBase
    {
        private readonly ICampingParkRepository _repo;
        private readonly IMapper _mapper;
        public CampingParksController(ICampingParkRepository repo, IMapper mapper)
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

        /// <summary>
        /// Get individual camping park.
        /// </summary>
        /// <param name="id"> The Id of the camping park</param>
        /// <returns></returns>
        [HttpGet ("{id:int}", Name = "GetCampingPark")]
        [ProducesResponseType(200, Type = typeof(CampingParkDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetCampingPark(int id)
        {
            var obj = _repo.GetCampingPark(id);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<CampingParkDTO>(obj);

            // This block does the same work as the line above. 
            /*var objDto = new NationalParkDto()
            {
                Created = obj.Created,
                Id = obj.Id,
                Name = obj.Name,
                State = obj.State,
                Established = obj.Established
            };*/

            return Ok(objDto);
        }

        /// <summary>
        /// Create a camping park.
        /// </summary>
        /// <param name="cParkDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CampingParkDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCampingPark([FromBody]CampingParkDTO cParkDto)
        {
            if(cParkDto == null)
            {
                return BadRequest(ModelState);
            }

            if(_repo.CampingParkExist(cParkDto.Name))
            {
                ModelState.AddModelError("", "Camping park with that name already exists!");
                return StatusCode(404, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cPark = _mapper.Map<CampingPark>(cParkDto);

            if(!_repo.CreateCampingPark(cPark))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record: {cPark.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCampingPark", new { id = cPark.Id }, cPark);
        }

        /// <summary>
        /// Update information on a existing camping park selected by it's Id.
        /// </summary>
        /// <param name="id"> The Id of the camping park</param>
        /// <param name="cParkDto"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}", Name = "UpdateCampingPark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCampingPark(int id, [FromBody]CampingParkDTO cParkDto)
        {
            if (cParkDto == null || id != cParkDto.Id)
            {
                return BadRequest(ModelState);
            }

            var cPark = _mapper.Map<CampingPark>(cParkDto);

            if (!_repo.UpdateCampingPark(cPark))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record: {cPark.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a camping park from the record by confirming it's Id.
        /// </summary>
        /// <param name="id"> The Id of the camping park</param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "DeleteCampingPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCampingPark(int id)
        {
            if (!_repo.CampingParkExist(id))
            {
                return BadRequest(ModelState);
            }

            var cPark = _repo.GetCampingPark(id);

            if (!_repo.DeleteCampingPark(cPark))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record: {cPark.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}