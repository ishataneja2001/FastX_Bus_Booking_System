using AutoMapper;
using FastXBookingSample.DTO;
using FastXBookingSample.Exceptions;
using FastXBookingSample.Interface;
using FastXBookingSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Bus Operator,User")]
    public class DroppingPointsController : ControllerBase
    {
        private readonly IDroppingPointRepository _droppingPointRepository;
        private readonly IBusRepository _busRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DroppingPointsController> _logger;

        public DroppingPointsController(IDroppingPointRepository droppingPointRepository, IBusRepository busRepository, IMapper mapper,ILogger<DroppingPointsController> logger)
        {
            _droppingPointRepository = droppingPointRepository;
            _busRepository = busRepository;
            _mapper = mapper;
            _logger = logger;
        }




        // GET: api/BoardingPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DroppingPointDto>>> GetDroppingPoints(int busid)
        {
            try
            {
                if (!_busRepository.BusExists(busid))
                    return BadRequest(ModelState);
                return Ok(_mapper.Map<List<DroppingPointDto>>(_droppingPointRepository.GetDroppingPointsByBusId(busid)));
            }catch (BusNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }





        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        // PUT: api/BoardingPoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> PutDroppingPoint(int id, DroppingPointDto droppingPointDto)
        {
            try
            {
                if (id != droppingPointDto.DroppingId)
                {
                    return BadRequest();
                }

                return Ok(_droppingPointRepository.UpdateDroppingPoints(id, _mapper.Map<DroppingPoint>(droppingPointDto)));
            }
            catch (DroppingPointNotFoundException ex)
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




        // POST: api/BoardingPoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<ActionResult<DroppingPointDto>> PostDroppingPoint(DroppingPointDto droppingPointDto)
        {
            try
            {
                return Ok(_droppingPointRepository.PostDroppingPoint(_mapper.Map<DroppingPoint>(droppingPointDto)));
            }
           
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }





        // DELETE: api/BoardingPoints/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteDroppingPoint(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(_droppingPointRepository.DeleteDroppingPoints(id));
            }
            catch (DroppingPointNotFoundException ex)
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
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<DroppingPoint> patchDroppingPoint)
        {
            try
            {
                return Ok(_droppingPointRepository.PatchDroppingPoint(id, patchDroppingPoint));
            }
            catch (DroppingPointNotFoundException ex)
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
