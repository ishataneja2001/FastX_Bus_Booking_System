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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository,IMapper mapper, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                return Ok(_mapper.Map<List<UserDto>>(_userRepository.GetAllUsers()));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }



        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
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
                user.Role = "User";
                return Ok(_userRepository.ModifyUserDetails(id, user));
            }
            catch (InvalidUsersPasswordException ex)
            {
                return StatusCode(406, ex.Message);
            }
            catch (InvalidUsersEmailException ex)
            {
                return StatusCode(406, ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }


        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userdto)
        {
            try
            {
                User user = _mapper.Map<User>(userdto);
                user.Role = "User";
                return Ok(_userRepository.PostUser(user));
            }
            catch (InvalidUsersPasswordException ex)
            {
                return StatusCode(406, ex.Message);
            }
            catch (InvalidUsersEmailException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(406, ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(409,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                return Ok(_userRepository.DeleteUser(id));
            }
            catch (UserNotFoundException ex)
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
        [Authorize(Roles = "User")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<User> patchuser)
        {
            try
            {
                return Ok(_userRepository.PatchUser(id, patchuser));
            }
            catch (UserNotFoundException ex)
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
