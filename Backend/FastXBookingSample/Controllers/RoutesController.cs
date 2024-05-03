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
using Microsoft.AspNetCore.JsonPatch;
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.Authorization;
using FastXBookingSample.Interface;

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Bus Operator")]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoutesController> _logger;

        public RoutesController(IRouteRepository routeRepository,IMapper mapper, ILogger<RoutesController> logger)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Routes
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<RouteDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<RouteDto>>> GetRoutesByBusId(int id)
        {
            try
            {
                return Ok(_mapper.Map<List<RouteDto>>(_routeRepository.GetRoutesByBusId(id)));
            }catch(BusNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        
       

        // PUT: api/Routes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutRoute(int id, RouteDto routedto)
        {
            try
            {
                if (id != routedto.RouteId)
                {
                    return BadRequest();
                }

                return Ok(_routeRepository.UpdateBusRoute(id, _mapper.Map<Models.Route>(routedto)));
            }
            catch (RouteNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        // POST: api/Routes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RouteDto>> PostRoute(RouteDto routedto)
        {
            try
            {
                return Ok(_routeRepository.PostBusRoute(_mapper.Map<Models.Route>(routedto)));
            }
            catch (RouteNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Routes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            try
            {
                return Ok(_routeRepository.DeleteBusRoute(id));
            }
            catch (RouteNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        //PATCH
        [HttpPatch("{id:int}")]
        public IActionResult PatchRoute(int id, [FromBody] JsonPatchDocument<Models.Route> patchRoute)
        {
            try
            {
                return Ok(_routeRepository.PatchRoute(id, patchRoute));
            }
            catch (RouteNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
