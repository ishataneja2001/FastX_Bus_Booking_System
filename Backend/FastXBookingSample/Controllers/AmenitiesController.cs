using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FastXBookingSample.Models;
using FastXBookingSample.DTO;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.Authorization;
using FastXBookingSample.Interface;


namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Bus Operator,User")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AmenitiesController> _logger;

        public AmenitiesController(IAmenityRepository amenityRepository,IMapper mapper, ILogger<AmenitiesController> logger)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDto>>> GetAmenities()
        {
            try
            {
                return Ok(_mapper.Map<List<AmenityDto>>(_amenityRepository.GetAllAmenities()));
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
            
        }


        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutAmenity(int id, AmenityDto amenitydto)
        {
            try
            {
                if (id != amenitydto.AmenityId)
                {
                    return BadRequest();
                }
                return Ok(_amenityRepository.UpdateAmenity(id, _mapper.Map<Amenity>(amenitydto)));
            }catch(AmenityNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AmenityDto>> PostAmenity(AmenityDto amenitydto)
        {
            try
            {
                return Ok(_amenityRepository.PostAmenity(_mapper.Map<Amenity>(amenitydto)));

            }
            catch(AmenityNotFoundException ex) 
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            try
            {
                return Ok(_amenityRepository.DeleteAmenity(id));

            }catch(AmenityNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }


        [HttpGet("GetAmenitiesByBusId/{busid}")]
        public async Task<ActionResult<List<AmenityDto>>> GetAmenitiesByBusId(int busid)
        {
            try
            {
                return Ok(_mapper.Map<List<AmenityDto>>(_amenityRepository.GetAllAmenitiesByBusId(busid)));

            }catch(BusNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Amenity> amenityPatch)
        {
 
            try
            {
                return Ok(_amenityRepository.PatchAmenity(id, amenityPatch));

            }catch (AmenityNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

        }
    }
}
