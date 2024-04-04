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
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusOperatorController : ControllerBase
    {
        private readonly IBusOperatorRepository _busOperatorRepository;
        private readonly IMapper _mapper;

        public BusOperatorController(IBusOperatorRepository busOperatorRepository, IMapper mapper)
        {
            _busOperatorRepository = busOperatorRepository;
            _mapper = mapper;
        }

        // GET: api/BusOperator
        [HttpGet]
        [Authorize(Roles = "Bus Operator,Admin")]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetBusOperators()
        {
            return Ok(_mapper.Map<List<UserDto>>(_busOperatorRepository.GetAllBusOperators()));
        }



        // PUT: api/BusOperator/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Bus Operator")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutUser(int id, UserDto userdto)
        {
            if (id != userdto.UserId)
            {
                return BadRequest();
            }
            User user = _mapper.Map<User>(userdto);
            user.Role = "Bus Operator";
            return Ok(_busOperatorRepository.ModifyBusOperatorDetails(id, user));
        }

        // POST: api/BusOperator
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userdto)
        {
            User user = _mapper.Map<User>(userdto);
            user.Role = "Bus Operator";
            return Ok(_busOperatorRepository.PostBusOperator(user));
        }

        // DELETE: api/BusOperator/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Bus Operator,Admin")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(_busOperatorRepository.DeleteBusOperator(id));
        }

        //PATCH
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Bus Operator")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<User> patchBusOperator)
        {
            return Ok(_busOperatorRepository.PatchBusOperator(id, patchBusOperator));
        }
    }
}
