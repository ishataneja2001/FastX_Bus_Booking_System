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
using Microsoft.AspNetCore.JsonPatch;
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.Authorization;
using FastXBookingSample.Interface;

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusOperatorController : ControllerBase
    {
        private readonly IBusOperatorRepository _busOperatorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BusOperatorController> _logger;

        public BusOperatorController(IBusOperatorRepository busOperatorRepository, IMapper mapper, ILogger<BusOperatorController> logger)
        {
            _busOperatorRepository = busOperatorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/BusOperator
        [HttpGet]
        [Authorize(Roles = "Bus Operator,Admin")]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetBusOperators()
        {
            try
            {
                return Ok(_mapper.Map<List<UserDto>>(_busOperatorRepository.GetAllBusOperators()));

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }



        // PUT: api/BusOperator/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Bus Operator")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutUser(int id, UserDto userdto)
        {
            try
            {
                if (id != userdto.UserId)
                {
                    return BadRequest();
                }
                User user = _mapper.Map<User>(userdto);
                user.Role = "Bus Operator";
                return Ok(_busOperatorRepository.ModifyBusOperatorDetails(id, user));
            }
            catch (InvalidUsersPasswordException ex)
            {
                return StatusCode(406, ex.Message);
            }
            catch (InvalidUsersEmailException ex)
            {
                return StatusCode(406, ex.Message);
            }
            catch (BusOperatorNotFoundException ex)
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

        // POST: api/BusOperator
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userdto)
        {
            try
            {
                User user = _mapper.Map<User>(userdto);
                user.Role = "Bus Operator";
                return Ok(_busOperatorRepository.PostBusOperator(user));
            }
            catch (InvalidUsersPasswordException ex)
            {
                return StatusCode(406, ex.Message);
            }
            catch (InvalidUsersEmailException ex)
            {
                return StatusCode(406,ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(409, ex.Message);
            }
            catch (BusOperatorNotFoundException ex)
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

        // DELETE: api/BusOperator/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Bus Operator,Admin")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                return Ok(_busOperatorRepository.DeleteBusOperator(id));
            }
            catch (BusOperatorNotFoundException ex)
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
        [Authorize(Roles = "Bus Operator")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<User> patchBusOperator)
        {
            try
            {
                return Ok(_busOperatorRepository.PatchBusOperator(id, patchBusOperator));
            }
            catch (BusOperatorNotFoundException ex)
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
