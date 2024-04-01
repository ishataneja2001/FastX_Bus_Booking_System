﻿using System;
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
    public class RoutesController : ControllerBase
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;

        public RoutesController(IRouteRepository routeRepository,IMapper mapper)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
        }

        // GET: api/Routes
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<RouteDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<RouteDto>>> GetRoutesByBusId(int id)
        {
            return Ok(_mapper.Map<List<RouteDto>>(_routeRepository.GetRoutesByBusId(id)));
        }

        
       

        // PUT: api/Routes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutRoute(int id, RouteDto routedto)
        {
            if (id != routedto.RouteId)
            {
                return BadRequest();
            }

           return Ok(_routeRepository.UpdateBusRoute(id,_mapper.Map<Models.Route>(routedto)));
        }

        // POST: api/Routes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RouteDto>> PostRoute(RouteDto routedto)
        {
          return Ok(_routeRepository.PostBusRoute(_mapper.Map<Models.Route>(routedto)));
        }

        // DELETE: api/Routes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            return Ok(_routeRepository.DeleteBusRoute(id));
        }

    }
}
