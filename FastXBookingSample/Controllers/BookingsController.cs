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
using Microsoft.AspNetCore.Authorization;

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingsController(BookingContext context, IBookingRepository bookingRepository,IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        // GET: api/Bookings
        [HttpGet]
        [Authorize(Roles = "Bus Operator,User")]
        [ProducesResponseType(200, Type = typeof(List<BookingDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsByBusId(int busid)
        {
          return Ok(_mapper.Map<List<BookingDto>>(_bookingRepository.GetAllBookingsByBusId(busid)));
        }


 
        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BookingDto>> PostBooking(BookingDto bookingdto)
        {
            return Ok(_bookingRepository.PostBooking(_mapper.Map<Booking>(bookingdto)));
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            return Ok(_bookingRepository.DeleteBooking(id));
        }

    }
}
