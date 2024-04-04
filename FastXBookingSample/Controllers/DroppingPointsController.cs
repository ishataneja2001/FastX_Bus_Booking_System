using AutoMapper;
using FastXBookingSample.DTO;
using FastXBookingSample.Models;
using FastXBookingSample.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Bus Operator")]
    public class DroppingPointsController : ControllerBase
    {
        private readonly IDroppingPointRepository _droppingPointRepository;
        private readonly IBusRepository _busRepository;
        private readonly IMapper _mapper;

        public DroppingPointsController(IDroppingPointRepository droppingPointRepository, IBusRepository busRepository, IMapper mapper)
        {
            _droppingPointRepository = droppingPointRepository;
            _busRepository = busRepository;
            _mapper = mapper;
        }




        // GET: api/BoardingPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DroppingPointDto>>> GetDroppingPoints(int busid)
        {
            if (!_busRepository.BusExists(busid))
                return BadRequest(ModelState);
            return Ok(_mapper.Map<List<DroppingPointDto>>(_droppingPointRepository.GetDroppingPointsByBusId(busid)));
        }





        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        // PUT: api/BoardingPoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> PutDroppingPoint(int id, DroppingPointDto droppingPointDto)
        {
            if (id != droppingPointDto.DroppingId)
            {
                return BadRequest();
            }

            return Ok(_droppingPointRepository.UpdateDroppingPoints(id, _mapper.Map<DroppingPoint>(droppingPointDto)));
        }




        // POST: api/BoardingPoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<ActionResult<DroppingPointDto>> PostDroppingPoint(DroppingPointDto droppingPointDto)
        {
            return Ok(_droppingPointRepository.PostDroppingPoint(_mapper.Map<DroppingPoint>(droppingPointDto)));
        }





        // DELETE: api/BoardingPoints/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteDroppingPoint(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(_droppingPointRepository.DeleteDroppingPoints(id));
        }

        //PATCH
        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<DroppingPoint> patchDroppingPoint)
        {
            return Ok(_droppingPointRepository.PatchDroppingPoint(id, patchDroppingPoint));
        }
    }
}
