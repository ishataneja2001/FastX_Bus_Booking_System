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
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminRepository adminRepository, IMapper mapper, ILogger<AdminController> logger)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Admin
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                return Ok(_mapper.Map<List<UserDto>>(_adminRepository.GetAllAdmin()));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }


        // PUT: api/Admin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(int id, UserDto userdto)
        {
            try
            {
                if (id != userdto.UserId)
                {
                    return BadRequest("User ID in the route does not match the UserID in the request body.");
                }
            
                User user = _mapper.Map<User>(userdto);
                user.Role = "Admin";
                return Ok(_adminRepository.ModifyAdminDetails(id, user));

            }
            catch (InvalidUsersPasswordException ex)
            {
                return StatusCode(406, ex.Message);
            }
            catch (InvalidUsersEmailException ex)
            {
                return StatusCode(406, ex.Message);
            }
            catch (AdminNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);

            }
             
            
        }


        // POST: api/Admin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> PostUser(UserDto userdto)
        {
            try
            {
                User user = _mapper.Map<User>(userdto);
                user.Role = "Admin";
                return Ok(_adminRepository.PostAdmin(user));
            }
            catch (InvalidUsersPasswordException ex)
            {
                return StatusCode(406, ex.Message);
            }
            catch (InvalidUsersEmailException ex)
            {
                return StatusCode(406, ex.Message);
            }
            catch (AdminNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            
        }

        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                return Ok(_adminRepository.DeleteAdmin(id));
            }catch (AdminNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            
        }
       
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<User> adminPatch)
        {
            try
            {
                return Ok(_adminRepository.PatchAdmin(id, adminPatch));
            }
            catch(AdminNotFoundException ex)
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
