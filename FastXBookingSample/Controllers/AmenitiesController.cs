using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FastXBookingSample.Models;
using FastXBookingSample.Repository;
using FastXBookingSample.DTO;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.Authorization;


namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Bus Operator")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public AmenitiesController(IAmenityRepository amenityRepository,IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDto>>> GetAmenities()
        {
            try
            {
                return Ok(_mapper.Map<List<AmenityDto>>(_amenityRepository.GetAllAmenities()));
            }catch(AmenityNotFoundException ex)
            {
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
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
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
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("GetAmenitiesByBusId/{busid}")]
        public async Task<ActionResult<List<AmenityDto>>> GetAmenitiesByBusId(int busid)
        {
            try
            {
                return Ok(_mapper.Map<List<AmenityDto>>(_amenityRepository.GetAllAmenitiesByBusId(busid)));

            }catch(AmenityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Amenity> amenityPatch)
        {
<<<<<<< HEAD
            return Ok(_amenityRepository.PatchAmenity(id, amenityPatch));
=======
            try
            {
                return Ok(_amenityRepository.PatchAmentity(id, amenityPatch));

            }catch (AmenityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
>>>>>>> 3a2283b693d8c6b70e8004699dee126b4d002311
        }
    }
}
