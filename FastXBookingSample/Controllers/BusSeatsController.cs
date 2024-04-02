using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FastXBookingSample.Models;
using FastXBookingSample.Repository;
using AutoMapper;
using FastXBookingSample.DTO;

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusSeatsController : ControllerBase
    {
        private readonly IBusSeatRepository _busSeatRepository;
        private readonly IMapper _mapper;

        public BusSeatsController(IBusSeatRepository busSeatRepository,IMapper mapper)
        {
            _busSeatRepository = busSeatRepository;
            _mapper = mapper;
        }

       

        // GET: api/BusSeats/5
        [HttpGet("{busid}")]
        [ProducesResponseType(200, Type = typeof(List<BusSeatDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<BusSeatDto>>> GetBusSeat(int busid)
        {
            return Ok(_mapper.Map<List<BusSeatDto>>(_busSeatRepository.GetSeatsByBusId(busid)));
        }

    }
}
