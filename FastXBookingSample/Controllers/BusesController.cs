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

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly IBusRepository _busrepository;
        private readonly IMapper _mapper;
        private readonly IBusSeatRepository _busSeatRepository;
        private readonly BookingContext _context;

        public BusesController(IBusRepository busrepository,IMapper mapper, IBusSeatRepository busSeatRepository,BookingContext context)
        {
            _busrepository = busrepository;
            _mapper = mapper;
            _busSeatRepository = busSeatRepository;
            _context = context;
        }

        // GET: api/Buses
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Bus>))]
        public async Task<ActionResult<IEnumerable<Bus>>> Getbuses()
        {
            try
            {
                var buses = _mapper.Map<List<BusDto>>(_busrepository.GetAll());
                if (buses == null)
                {
                    return NotFound();
                }
                return Ok(buses);
            }catch (NoBusAvailableException ex)
            {
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        // GET: api/Buses/5
        [HttpGet("{id}")]
        [ProducesResponseType(200,Type = typeof(Bus))]
        public async Task<ActionResult<Bus>> GetBus(int id)
        {
            try
            {
                var bus = _mapper.Map<BusDto>(_busrepository.GetBusById(id));
                if (bus == null)
                {
                    return NotFound();
                }
                return Ok(bus);
            }
            catch(NoBusAvailableException ex) 
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Buses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(200,Type=typeof(String))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutBus(int id, BusDto busdto)
        {
            try
            {
                if (id != busdto.BusId)
                {
                    return BadRequest();
                }
                if (!_busrepository.BusExists(id))
                    return NotFound();
                Bus existingBus = await _busrepository.GetBusById(id);
                if (existingBus.NoOfSeats != busdto.NoOfSeats)
                {
                    _busSeatRepository.DeleteSeatsByBusId(busdto.BusId);
                    _busSeatRepository.AddSeatByBusId(busdto.BusId, busdto.NoOfSeats);
                }
                _context.Entry(existingBus).State = EntityState.Detached;

                var bus = _mapper.Map<Bus>(busdto);



                return Ok(_busrepository.UpdateBus(id, bus));
            }
            catch (NoBusAvailableException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Buses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200 ,Type=typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Bus>> PostBus(BusDto busdto)
        {
            try
            {
                if (busdto == null)
                    return BadRequest(ModelState);
                if (!_busrepository.RoleExists(busdto.BusOperator))
                    return BadRequest(ModelState);
                var bus = _mapper.Map<Bus>(busdto);
                string message = _busrepository.CreateBus(bus);
                _busSeatRepository.AddSeatByBusId(bus.BusId, bus.NoOfSeats);

                return Ok(message);
            }
            catch (NoBusAvailableException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Buses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteBus(int id)
        {
            try
            {
                if (!_busrepository.BusExists(id))
                    return NotFound();
                _busSeatRepository.DeleteSeatsByBusId(id);
                string message = _busrepository.DeleteBus(id);

                return Ok(message);
            }
            catch (NoBusAvailableException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetBusByDetails")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Bus>))]
        public async Task<ActionResult<IEnumerable<Bus>>> GetBusByDetails([FromQuery] string origin, [FromQuery] string destination, [FromQuery] DateOnly date)
        {
            try
            {
                var buses = _mapper.Map<List<BusDto>>(_busrepository.GetBusByDetails(origin, destination, date));
                if (buses == null)
                    return BadRequest(ModelState);
                return Ok(buses);
            }
            catch (NoBusAvailableException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("PostBusAmenities")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BusAmenity>> PostBusAmenity(int busid,int amenityid)
        {
            try
            {
                return Ok(_busrepository.AddBusAmenity(busid, amenityid));

            }catch (NoBusAvailableException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //Patch
        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Bus> patchBus)
        {
            try
            {
                return Ok(_busrepository.PatchBus(id, patchBus));

            }
            catch (NoBusAvailableException ex)
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
