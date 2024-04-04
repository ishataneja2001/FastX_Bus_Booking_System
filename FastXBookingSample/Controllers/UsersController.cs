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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                return Ok(_mapper.Map<List<UserDto>>(_userRepository.GetAllUsers()));
            }catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
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
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //PATCH
        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<User> patchuser)
        {
            try
            {
                return Ok(_userRepository.PatchUser(id, patchuser));
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
    }
}
