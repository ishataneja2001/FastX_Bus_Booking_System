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
    [Authorize(Roles = "Bus Operator,User,Admin")]
    public class BookingHistoriesController : ControllerBase
    {
        private readonly IBookingHistoryRepository _bookingHistoryRepository;
        private readonly IMapper _mapper;

        public BookingHistoriesController(IBookingHistoryRepository bookingHistoryRepository,IMapper mapper)
        {
            _bookingHistoryRepository = bookingHistoryRepository;
            _mapper = mapper;
        }

        // GET: api/BookingHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List<BookingHistoryDto>>>> GetBookingHistories()
        {
            return Ok(_mapper.Map<List<BookingHistoryDto>>(_bookingHistoryRepository.GetAll()));
        }

        
    }
}
