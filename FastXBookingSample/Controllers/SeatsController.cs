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
<<<<<<< HEAD
using FastXBookingSample.Exceptions;
=======
using Microsoft.AspNetCore.Authorization;
>>>>>>> 36f6d31aa2c2cadaf071a38927f8aa56335cb1c7

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
        [Authorize(Roles = "Bus Operator,Admin,User")]
        [ProducesResponseType(200, Type = typeof(List<SeatDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<SeatDto>>> GetSeatsByBookingId(int bookingId)
        {
            try
            {
                return Ok(_mapper.Map<List<SeatDto>>(_seatRepository.GetSeatsByBookingId(bookingId)));
            }catch(NoSeatsAvailableException ex)
            {
                return NotFound(ex.Message);
            }catch(NoBookingAvailableException ex)
            {
                return NotFound(ex.Message);
            }catch (Exception ex)
            {
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
            catch (NoSeatsAvailableException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
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
                _seatRepository.PostSeatByBookingId(_mapper.Map<Seat>(seatDto));
                return NoContent();
            }
            catch (NoSeatsAvailableException ex)
            {
                return NotFound(ex.Message);
            }          
            catch (Exception ex)
            {
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
            catch (NoSeatsAvailableException ex)
            {
                return NotFound(ex.Message);
            }
            
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }
    }
}
