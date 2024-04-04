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
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        // GET: api/Admin
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return Ok(_mapper.Map<List<UserDto>>(_adminRepository.GetAllAdmin()));
        }


        // PUT: api/Admin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(int id, UserDto userdto)
        {
            if (id != userdto.UserId)
            {
                return BadRequest();
            }
            User user = _mapper.Map<User>(userdto);
            user.Role = "Admin";
            return Ok(_adminRepository.ModifyAdminDetails(id, user));
        }


        // POST: api/Admin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> PostUser(UserDto userdto)
        {
            User user = _mapper.Map<User>(userdto);
            user.Role = "Admin";
            return Ok(_adminRepository.PostAdmin(user));
        }

        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            return Ok(_adminRepository.DeleteAdmin(id));
        }

        
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<User> adminPatch)
        {
            return Ok(_adminRepository.PatchAdmin(id, adminPatch));
        }
    }
}
