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
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.Authorization;
using FastXBookingSample.Interface;

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SeatsController> _logger;

        public SeatsController(ISeatRepository seatRepository,IMapper mapper, ILogger<SeatsController> logger)
        {
            _seatRepository = seatRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Seats
        [HttpGet]
        [Authorize(Roles = "Bus Operator,Admin,User")]
        [ProducesResponseType(200, Type = typeof(List<SeatDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<SeatDto>>> GetSeatsByBookingId(int bookingId)
        {
            try
            {
                return Ok(_mapper.Map<List<SeatDto>>(_seatRepository.GetSeatsByBookingId(bookingId)));
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);

            }
        }

        [HttpGet("GetSeatsByUserId")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(200, Type = typeof(List<SeatDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<SeatDto>>> GetSeatsByUserId(int userId)
        {
            try
            {
                return Ok(_mapper.Map<List<SeatDto>>(_seatRepository.GetSeatsByUserId(userId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);

            }
        }


       
        // POST: api/Seats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SeatDto>> PostSeat(SeatDto seatDto)
        {
            try
            {
                return Ok(_seatRepository.PostSeatByBookingId(_mapper.Map<Seat>(seatDto)));
                
            }        
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);

            }
        }

        // DELETE: api/Seats/5
        [HttpDelete("{seatId}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteSeatByBookingId(int seatId)
        {
            try
            {
                _seatRepository.DeleteSeatBySeatId(seatId);
                return NoContent();
            }
        
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);

            }
        }
    }
}
