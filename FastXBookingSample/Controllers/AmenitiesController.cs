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

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(_mapper.Map<List<AmenityDto>>(_amenityRepository.GetAllAmenities()));
        }


        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutAmenity(int id, AmenityDto amenitydto)
        {
            if (id != amenitydto.AmenityId)
            {
                return BadRequest();
            }
            return Ok(_amenityRepository.UpdateAmenity(id, _mapper.Map<Amenity>(amenitydto)));
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AmenityDto>> PostAmenity(AmenityDto amenitydto)
        {
            return Ok(_amenityRepository.PostAmenity(_mapper.Map<Amenity>(amenitydto)));
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            return Ok(_amenityRepository.DeleteAmenity(id));
        }


        [HttpGet("GetAmenitiesByBusId/{busid}")]
        public async Task<ActionResult<List<AmenityDto>>> GetAmenitiesByBusId(int busid)
        {
            return Ok(_mapper.Map<List<AmenityDto>>(_amenityRepository.GetAllAmenitiesByBusId(busid)));
        }
    }
}
