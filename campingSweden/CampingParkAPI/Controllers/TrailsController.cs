using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampingParkAPI.Models;
using CampingParkAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrailAPI.Repository.IRepository;

namespace TrailAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "CampingParkOpenAPITrails")] // This bundles the Swagger UI Document for this controller only.
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepository _repo;
        private readonly IMapper _mapper;
        public TrailsController(ITrailRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of all trails.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrailDTO>))]
        public IActionResult GetTrails()
        {
            var objList = _repo.GetAllTrails();

            var objDto = _mapper.Map<ICollection<Trail>, List<TrailDTO>>(objList);

            return Ok(objDto);
        }

        /// <summary>
        /// Get individual trail.
        /// </summary>
        /// <param name="id"> The Id of the trail</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int id)
        {
            var obj = _repo.GetTrail(id);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<TrailDTO>(obj);

            return Ok(objDto);
        }

        /// <summary>
        /// Create a trail.
        /// </summary>
        /// <param name="trailDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody]TrailCreateDTO trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_repo.TrailExist(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail with that name already exists!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trail = _mapper.Map<Trail>(trailDto);

            if (!_repo.CreateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record: {trail.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetTrail", new { id = trail.Id }, trail);
        }

        /// <summary>
        /// Update information on a existing trail selected by it's Id.
        /// </summary>
        /// <param name="id"> The Id of the trail</param>
        /// <param name="trailDto"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}", Name = "UpdateTrail")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int id, [FromBody]TrailUpdateDTO trailDto)
        {
            if (trailDto == null || id != trailDto.Id)
            {
                return BadRequest(ModelState);
            }

            var trail = _mapper.Map<Trail>(trailDto);

            if (!_repo.UpdateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record: {trail.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a trail from the record by confirming it's Id.
        /// </summary>
        /// <param name="id"> The Id of the trail</param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int id)
        {
            if (!_repo.TrailExist(id))
            {
                return BadRequest(ModelState);
            }

            var trail = _repo.GetTrail(id);

            if (!_repo.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record: {trail.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}