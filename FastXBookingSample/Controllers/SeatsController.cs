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
    public class SeatsController : ControllerBase
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IMapper _mapper;

        public SeatsController(ISeatRepository seatRepository,IMapper mapper)
        {
            _seatRepository = seatRepository;
            _mapper = mapper;
        }

        // GET: api/Seats
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<SeatDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<SeatDto>>> GetSeatsByBookingId(int bookingId)
        {
            return Ok(_mapper.Map<List<SeatDto>>(_seatRepository.GetSeatsByBookingId(bookingId)));
        }

        [HttpGet("GetSeatsByUserId")]
        [ProducesResponseType(200, Type = typeof(List<SeatDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<SeatDto>>> GetSeatsByUserId(int userId)
        {
            return Ok(_mapper.Map<List<SeatDto>>(_seatRepository.GetSeatsByUserId(userId)));
        }


       
        // POST: api/Seats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SeatDto>> PostSeat(SeatDto seatDto)
        {
            _seatRepository.PostSeatByBookingId(_mapper.Map<Seat>(seatDto));
            return NoContent();
        }

        // DELETE: api/Seats/5
        [HttpDelete("{bookingid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteSeatByBookingId(int bookingid)
        {
            _seatRepository.DeleteSeatByBookingId(bookingid);
            return NoContent();
        }
    }
}
