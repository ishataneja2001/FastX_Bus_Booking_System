using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FastXBookingSample.Models;
using AutoMapper;
using FastXBookingSample.DTO;
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.Authorization;
using FastXBookingSample.Interface;


namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Bus Operator,User,Admin")]
    public class BookingHistoriesController : ControllerBase
    {
        private readonly IBookingHistoryRepository _bookingHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BoardingPointsController> _logger;

        public BookingHistoriesController(IBookingHistoryRepository bookingHistoryRepository,IMapper mapper, ILogger<BoardingPointsController> logger)
        {
            _bookingHistoryRepository = bookingHistoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/BookingHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List<BookingHistoryDto>>>> GetBookingHistories()
        {
            try
            {
                return Ok((_bookingHistoryRepository.GetAll()));

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }


        [HttpGet("getAllBookingsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<List<BookingHistoryDto>>>> GetAllBookingsByUserId(int userId)
        {
            try
            {
                return Ok((_bookingHistoryRepository.GetAllBookingsByUserId(userId)));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getCancelledBookingsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<List<BookingHistoryDto>>>> GetCancelledBookingsByUserId(int userId)
        {
            try
            {
                return Ok((_bookingHistoryRepository.GetCancelledBookingsByUserId(userId)));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getAllBookingsByBusId/{busId}")]
        public async Task<ActionResult<IEnumerable<List<BookingHistoryDto>>>> GetAllBookingsByBus(int busId)
        {
            try
            {
                return Ok((_bookingHistoryRepository.GetAllBookingsByBus(busId)));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }


    }
}
