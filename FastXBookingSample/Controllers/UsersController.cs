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
          
            return Ok(_mapper.Map<List<UserDto>>(_userRepository.GetAllUsers()));
        }



        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutUser(int id, UserDto userdto)
        {
            if (userdto.Role != "User")
                return BadRequest();
            if (id != userdto.UserId)
            {
                return BadRequest();
            }

            return Ok(_userRepository.ModifyUserDetails(id,_mapper.Map<User>(userdto)));
        }


        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userdto)
        {
            if (userdto.Role != "User")
            {
                ModelState.AddModelError("", "Role Should be user");
                return StatusCode(408,ModelState);
            }
            return Ok(_userRepository.PostUser(_mapper.Map<User>(userdto)));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(_userRepository.DeleteUser(id));
        }

        
    }
}
